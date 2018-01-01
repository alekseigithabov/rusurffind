using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TabTextFinder.Finder
{
    // This class should be instantiated while the file is locked by this process.
    // Otherwise, the consistency of data & tag may not be assured.
    sealed class FileInfoComparer : IEqualityComparer<FileInfo>
    {
        public bool Equals( FileInfo a, FileInfo b )
        {
            if (a == null || b == null) { return a == null && b == null; }
            return a.FullName == b.FullName && a.Length == b.Length && a.LastWriteTime == b.LastWriteTime;
        }

        public int GetHashCode( FileInfo info )
        {
            return info.GetHashCode();
        }
    }

    #region file caches

    interface IFileCache
    {
        FileInfo Info { get; }
        long Size { get; }              // the size in bytes
        uint Generation { get; set; }   // the last generation when this cache was used
    }

    sealed class FileEncodingCache : IFileCache
    {
        public FileInfo Info { get; private set; }
        public long Size { get { return 0; } }         // to cache this always
        public uint Generation { get; set; }
        public string Encoding { get; private set; }

        public FileEncodingCache( FileInfo info, string encoding )
        {
            Info = info;
            Encoding = encoding;
        }
    }

    sealed class FileContentCache : IFileCache
    {
        public FileInfo Info { get; private set; }
        public long Size { get { return (string.IsNullOrEmpty( Content ) ? 0 : (long) Content.Length * 2/*char*/); } }
        public uint Generation { get; set; }
        public string Encoding { get; private set; }
        public string Content { get; private set; }

        public FileContentCache( FileInfo info, string encoding, string content )
        {
            Info = info;
            Encoding = encoding;
            Content = content;
        }
    }

    sealed class FileWeakContentCache : IFileCache
    {
        public FileInfo Info { get; private set; }
        public long Size { get { return 0; } }
        public uint Generation { get; set; }

        private string encoding;
        private WeakReference weak_content;

        public FileWeakContentCache( FileContentCache cache )
        {
            Info = cache.Info;
            encoding = cache.Encoding;
            weak_content = new WeakReference( cache.Content );
        }

        public FileContentCache Lock()
        {
            string content = (string) weak_content.Target;
            if (content == null) { return null; }
            return new FileContentCache( Info, encoding, content );
        }
    }

    #endregion
    #region cache map

    class FileCacheMap<T> where T : IFileCache
    {
        public uint Generation { get; set; }
        public long MaxSize { get; private set; }
        public long Size { get; private set; }
        public int Count { get { return dict.Count; } }

        private Dictionary<string, T> dict = new Dictionary<string, T>();

        public FileCacheMap( long maxsize )
        {
            Generation = 0;
            MaxSize = maxsize;
        }

        public bool Contains( string key )
        {
            return dict.ContainsKey( key );
        }

        public bool Get( string key, out T cache )
        {
            if (dict.TryGetValue( key, out cache )) {
                cache.Generation = Generation;
                return true;
            }
            return false;
        }

        public void Add( string key, T cache )
        {
            dict.Add( key, cache );
            cache.Generation = Generation;
            Size += cache.Size;
        }

        public bool Remove( string key )
        {
            T cache = dict[key];
            if (cache == null) { return false; }

            Size -= cache.Size;
            return dict.Remove( key );
        }

        // Throw away old caches whose generation is older than current generation by more than `threshold'
        public void Purge( int threshold )
        {
            if (Generation < threshold) { return; }

            // List up unreferenced cache keys anticipating that all caches are referenced everytime hopefully
            List<string> paths = new List<string>();
            foreach (string path in dict.Keys) {
                T cache = dict[path];
                if (Generation - cache.Generation >= threshold) {
                    paths.Add( path );
                }
            }
            foreach (string path in paths) {
                Remove( path );
            }

            CheckSize();
        }

        private bool CheckSize()
        {
            long size = 0;
            foreach (T cache in dict.Values) {
                size += cache.Size;
            }
            Debug.Assert( Size == size );
            return (Size == size);
        }
    }

    static class FileCacheMapUtil
    {
        private static FileInfoComparer comparer = new FileInfoComparer();

        private static string getKey( FileInfo info, string auxkey )
        {
            return string.Format( "{0}?<{1}>", info.FullName, auxkey );  // use chars that are not allowed for file path as delimiters
        }

        public static void IncrementGeneration<T>( FileCacheMap<T> mapCache ) where T : IFileCache
        {
            if (mapCache == null) { return; }
            mapCache.Generation++;
        }

        public static T GetCache<T>( FileCacheMap<T> mapCache, FileInfo info, string auxkey ) where T : IFileCache
        {
            if (mapCache == null || mapCache.Count == 0) { return default( T ); }

            lock (mapCache) {
                T cache;
                string key = getKey( info, auxkey );
                if (mapCache.Get( key, out cache )) {
                    if (comparer.Equals( cache.Info, info )) {
                        return cache;
                    }
                    // stale cache
                    mapCache.Remove( key );
                }
            }
            return default( T );
        }

        public static T AddCache<T>( FileCacheMap<T> mapCache, T cache, string auxkey ) where T : IFileCache
        {
            if (mapCache == null) { return cache; }

            lock (mapCache) {
                string key = getKey( cache.Info, auxkey );
                // In this application, the same path would hardly be added in a single find operation.
                // Thus, no racing would not occur in the following Add operation.
                // But just in case, use the last one
                if (mapCache.Contains( key )) {
                    mapCache.Remove( key );
                }
                if (mapCache.Size + cache.Size <= mapCache.MaxSize) {
                    mapCache.Add( key, cache );
                }
            }
            return cache;
        }
    }

    #endregion

    class FileLock : IDisposable
    {
        public bool Locked { get { return fs != null; } }

        private FileStream fs;

        public FileLock( string path )
        {
            try {
                fs = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read );
            }
            catch (Exception) {
            }
        }

        public void Dispose()
        {
            if (fs != null) {
                fs.Close();
                fs.Dispose();
                fs = null;
            }
        }
    }

    class FileCacheManager
    {
        private FileCacheMap<FileEncodingCache> mapEncoding;
        private FileCacheMap<FileContentCache> mapContent;
        private FileCacheMap<FileWeakContentCache> mapWeakContent;

        private const int threshold = 3;

        public void IncrementGeneration()
        {
            FileCacheMapUtil.IncrementGeneration( mapEncoding );
            FileCacheMapUtil.IncrementGeneration( mapContent );
            FileCacheMapUtil.IncrementGeneration( mapWeakContent );
        }

        #region cache map

        public void SetPolicy( bool cache_content, long cache_size )
        {
            long maxsize = cache_size * 1024 * 1024;
            renewCacheMap( ref mapEncoding, true, 0 );
            renewCacheMap( ref mapContent, cache_content, maxsize );
            renewCacheMap( ref mapWeakContent, !cache_content, maxsize );
        }

        public void PurgeElderCache()
        {
            if (mapEncoding != null) {
                mapEncoding.Purge( threshold * 3 ); // encodings have a longer lifetime
            }
            if (mapContent != null) {
                mapContent.Purge( threshold );
            }
            if (mapWeakContent != null) {
                mapWeakContent.Purge( threshold );
            }
        }

        private void renewCacheMap<T>( ref FileCacheMap<T> mapCache, bool bEnable, long maxsize ) where T : IFileCache
        {
            if (bEnable) {
                mapCache = new FileCacheMap<T>( maxsize );
            } else {
                mapCache = null;
            }
        }

        #endregion
        #region cache encoding / content

        // no need of FileLock, just take a look at cache
        private FileEncodingCache getCachedEncoding( FileInfo info )
        {
            FileEncodingCache cache = FileCacheMapUtil.GetCache( mapEncoding, info, string.Empty );
            if (cache != null) { return cache; }

            return null;
        }

        // no need of FileLock, just take a look at cache
        private FileContentCache getCachedContent( FileInfo info, string encoding )
        {
            FileContentCache cache = FileCacheMapUtil.GetCache( mapContent, info, encoding );
            if (cache != null) { return cache; }

            FileWeakContentCache wcache = FileCacheMapUtil.GetCache( mapWeakContent, info, encoding );
            if (wcache != null) {
                cache = wcache.Lock();
                if (cache != null) { return cache; }
            }

            return null;
        }

        // need FileLock before calling
        private FileEncodingCache getOrCreateEncodingCache( FileInfo info )
        {
            FileEncodingCache cache = getCachedEncoding( info );
            if (cache != null) { return cache; }

            try {
                using (FileStream fs = new FileStream( info.FullName, FileMode.Open, FileAccess.Read, FileShare.Read )) {
                    //Dobon.Jcode jcode = new Dobon.Jcode();
                    //Encoding enc = jcode.GetCode( fs );
                    //if (enc == null) { return null; }
                    //return FileCacheMapUtil.AddCache( mapEncoding, new FileEncodingCache( info, enc.WebName ), string.Empty );
                    return null;
                }
            }
            catch (Exception) {
                return null;
            }
        }

        // need FileLock before calling
        // the encoding should explicitly be specified
        private FileContentCache getOrCreateContentCache( FileInfo info, string encoding )
        {
            if (string.IsNullOrEmpty( encoding )) { return null; }

            FileContentCache cache = getCachedContent( info, encoding );
            if (cache != null) { return cache; }

            try {
                // with Azuki, looks following is just suffice
                Encoding enc = Encoding.GetEncoding( encoding, EncoderFallback.ReplacementFallback, DecoderFallback.ReplacementFallback );

                // this method consumes unexpectedly big memory?
                //using (StreamReader reader = new StreamReader( info.FullName, enc, false, 1 << 14 )) {
                //    string content = reader.ReadToEnd();
                //}

                byte[] bytes = File.ReadAllBytes( info.FullName );
                string content = enc.GetString( bytes );

                cache = new FileContentCache( info, encoding, content );
                if (mapWeakContent != null) {
                    FileCacheMapUtil.AddCache( mapWeakContent, new FileWeakContentCache( cache ), encoding );
                }
                return FileCacheMapUtil.AddCache( mapContent, cache, encoding );
            }
            catch (Exception) {
                return null;
            }
        }

        #endregion

        // read the whole file contenet for preview
        // the encoding should explicitly be specified
        public FileContentCache GetContent( string file, string encoding )
        {
            if (string.IsNullOrEmpty( encoding )) { return null; }
            using (FileLock flock = new FileLock( file )) {
                if (!flock.Locked) { return null; }
                try {
                    FileInfo info = new FileInfo( file );
                    return getOrCreateContentCache( info, encoding );
                }
                catch (Exception) {
                    return null;
                }
            }
        }

        // return the reader from string when cached, or from file, for text search
        public TextReader GetContentReader( string path, string forced_encoding, ref FoundFile file, out string content )
        {
            bool encoding_forced = !string.IsNullOrEmpty( forced_encoding );

            content = null;

            // FileLock takes a longer time, so check the cache first
            try {
                FileInfo info = new FileInfo( path );
                string encoding = forced_encoding;
                if (string.IsNullOrEmpty( encoding )) {
                    FileEncodingCache cache = getCachedEncoding( info );
                    if (cache != null) {
                        encoding = cache.Encoding;
                    }
                }
                if (string.IsNullOrEmpty( encoding ) == false) {
                    FileContentCache cache = getCachedContent( info, encoding );
                    if (cache != null) {
                        file = new FoundFile( cache.Info, cache.Encoding, encoding_forced );
                        content = cache.Content;
                        return new StringReader( content );
                    }
                }
            }
            catch (Exception) {
                return null;
            }

            using (FileLock flock = new FileLock( path )) {
                if (!flock.Locked) { return null; }
                try {
                    FileInfo info = new FileInfo( path );
                    string encoding = forced_encoding;
                    if (string.IsNullOrEmpty( encoding )) {
                        FileEncodingCache cache = getOrCreateEncodingCache( info );
                        if (cache == null) { return null; }
                        encoding = cache.Encoding;
                    }
                    if (mapContent != null && mapContent.Size + info.Length * 2 <= mapContent.MaxSize) {
                        FileContentCache cache = getOrCreateContentCache( info, encoding );
                        if (cache == null) { return null; }

                        file = new FoundFile( cache.Info, cache.Encoding, encoding_forced );
                        content = cache.Content;
                        return new StringReader( content );
                    } else {
                        // with Azuki, looks following is just suffice
                        Encoding enc = Encoding.GetEncoding( encoding, EncoderFallback.ReplacementFallback, DecoderFallback.ReplacementFallback );
                        file = new FoundFile( info, enc.WebName, encoding_forced );
                        return new StreamReader( path, enc );
                    }
                }
                catch (Exception) {
                    return null;
                }
            }
        }
    }

    static class FileCache
    {
        public static FileCacheManager Instance { get { return cache; } }
        private static readonly FileCacheManager cache = new FileCacheManager();
    }
}
