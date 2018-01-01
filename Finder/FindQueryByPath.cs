using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TabTextFinder.Finder
{
    sealed class FindPathParam
    {
        public string Root { get; private set; }
        public string[] FilePatterns { get; private set; }
        public string[] ExcludeFilePatterns { get; private set; }
        public bool Recursive { get; private set; }

        private static char[] separators = new char[] { ' ' };

        public FindPathParam( string root, string pattern, string exclude_pattern, bool recursive )
        {
            Root = root;
            FilePatterns = pattern.Split( separators );
            ExcludeFilePatterns = exclude_pattern.Split( separators );
            Recursive = recursive;
        }
    }

    class FindQueryByPath : FindQuery
    {
        private FindPathParam param;
        private string forced_encoding;

        private const bool async = true;
        private const int queue_capacity = 1024;

        private Queue<string> queue;
        private bool done_queueing;

        public FindQueryByPath( FindType type, FoundLineOption line_option, TextFinder finder, TextFinder keyword_finder, string file_encoding, FindPathParam _param )
            : base( type, line_option, finder, keyword_finder )
        {
            param = _param;
            forced_encoding = file_encoding;
            queue = new Queue<string>( queue_capacity );
        }

        public FindQueryByPath( FindType type, FoundLineOption line_option, TextFinder finder, TextFinder keyword_finder, string file_encoding, FindQuery base_query )
            : base( type, line_option, finder, keyword_finder )
        {
            forced_encoding = file_encoding;
            queue = new Queue<string>( base_query.FoundFiles.Count );
            foreach (FoundFile ff in base_query.FoundFiles) {
                queue.Enqueue( ff.Info.FullName );
            }
        }

        public override void Find()
        {
            FileCache.Instance.IncrementGeneration();

            string[] roots = (param == null) ? new string[0]: param.Root.Split( new char[] { '|' } );

            if (async) {
                int count = NumThreads;
                Action<int>[] actions = new Action<int>[count];
                IAsyncResult[] ars = new IAsyncResult[count];
                WaitHandle[] handles = new WaitHandle[count];
                for (int i = 0; i < count; ++i) {
                    actions[i] = new Action<int>( ProcQueueAsync );
                    ars[i] = actions[i].BeginInvoke( i, null, null );
                    handles[i] = ars[i].AsyncWaitHandle;
                }

                foreach (string root in roots) {
                    SearchDirectory( root );
                }
                done_queueing = true;

                while (!WaitHandle.WaitAll( handles, 50 )) {
                    Finder.Abort |= Abort;
                }
                for (int i = 0; i < count; ++i) {
                    actions[i].EndInvoke( ars[i] );
                }
            } else {
                foreach (string root in roots) {
                    SearchDirectory( root );
                }
                ProcQueueSync( 0 );
            }

            FileCache.Instance.PurgeElderCache();
            Working = false;
        }

        private List<string> FindMatchedFiles( string directory, string[] patterns )
        {
            List<string> files = new List<string>();
            foreach (string pattern in patterns) {
                if (Abort) { return files; }
                if (pattern.Length == 0) { continue; }

                // find files that match the wildcard patterns
                try {
                    string[] _files = Directory.GetFiles( directory, pattern );
                    foreach (string file in _files) {
                        int idx = files.BinarySearch( file );
                        if (idx < 0) {
                            files.Insert( ~idx, file );
                        }
                    }
                }
                catch (Exception) {
                    continue;
                }
            }
            return files;
        }

        private void SearchDirectory( string directory )
        {
            List<string> includes = FindMatchedFiles( directory, param.FilePatterns );
            List<string> excludes = FindMatchedFiles( directory, param.ExcludeFilePatterns );
            if (Abort) { return; }

            foreach (string exclude in excludes) {
                int idx = includes.BinarySearch( exclude );
                if (idx >= 0) {
                    includes.RemoveAt( idx );
                }
            }

            if (async) {
                SearchFilesAsync( includes );
            } else {
                SearchFilesSync( includes );
            }

            if (param.Recursive) {
                if (Abort) { return; }

                string[] dirs = null;
                try {
                    dirs = Directory.GetDirectories( directory );
                }
                catch (Exception) {
                    return;
                }

                foreach (string dir in dirs) {
                    if (Abort) { return; }
                    SearchDirectory( dir );
                }
            }
        }

        private void SearchFilesSync( List<string> files )
        {
            foreach (string file in files) {
                queue.Enqueue( file );
            }
        }

        private void SearchFilesAsync( List<string> files )
        {
            int bunch = NumThreads;
            int pos = 0;
            int end = files.Count;
            while (!Abort && pos < end) {
                int cnt = end - pos;
                while (true) {
                    int room = queue_capacity - queue.Count;
                    if (room >= cnt) { break; }
                    if (room >= bunch) { break; }
                    if (room >= queue_capacity) { break; }
                    Thread.Sleep( 10 );
                }

                Monitor.Enter( queue );
                try {
                    cnt = Math.Min( cnt, queue_capacity - queue.Count );
                    for (int i = 0; i < cnt; ++i) {
                        queue.Enqueue( files[pos++] );
                    }
                }
                finally {
                    Monitor.PulseAll( queue );
                    Monitor.Exit( queue );
                }
            }
        }

        private void SearchFile( string path )
        {
            FoundFile file = null;
            FoundLineCollection lines;
            int num_lines = 0;
            string content;
            using (TextReader reader = FileCache.Instance.GetContentReader( path, forced_encoding, ref file, out content )) {
                if (reader == null) { return; }
                lines = Finder.FindInLines( reader, content, out num_lines );
            }
            AppendFoundForFile( file, lines, num_lines, file.Info.Length );
        }

        private void ProcQueueSync( int id )
        {
            while (!Abort && queue.Count > 0) {
                SearchFile( queue.Dequeue() );
            }
        }

        private void ProcQueueAsync( int id )
        {
            while (true) {
                string path = null;
                Monitor.Enter( queue );
                try {
                    while (queue.Count == 0) {
                        if (done_queueing) { return; }
                        Monitor.Wait( queue, 10 );
                    }
                    path = queue.Dequeue();
                }
                finally {
                    Monitor.Exit( queue );
                }

                SearchFile( path );
            }
        }
    }
}
