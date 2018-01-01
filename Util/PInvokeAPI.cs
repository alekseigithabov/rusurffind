using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TabTextFinder.Util.PInvokeAPI
{
    static class SystemInfo
    {
        [DllImport( "kernel32" )]
        private static extern void GetSystemInfo( ref SYSTEM_INFO pinfo );

        [StructLayout( LayoutKind.Sequential )]
        public struct SYSTEM_INFO
        {
            public int dwOemId;
            public int dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public int dwNumberOfProcessors;
            public int dwProcessorType;
            public int dwAllocationGranularity;
            public short dwProcessorLevel;
            public short dwProcessorRevision;
        }

        public static int NumberOfProcessors
        {
            get
            {
                SYSTEM_INFO info = new SYSTEM_INFO();
                GetSystemInfo( ref info );
                return info.dwNumberOfProcessors;
            }
        }
    }

    static class WM
    {
        [DllImport( "user32" )]
        private static extern IntPtr SendMessage( IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam );

        [DllImport( "user32.dll", SetLastError = true )]
        private static extern IntPtr PostMessage( IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam );

        public const uint SETREDRAW = 0x000B;
        public const uint PAINT = 0x000F;
        public const uint KEYDOWN = 0x0100;
        public const uint SYSKEYDOWN = 0x0104;
        public const uint USER = 0x0400;

        public static void SetRedraw( Control control, bool enable )
        {
            SendMessage( control.Handle, SETREDRAW, new IntPtr( (enable) ? 1 : 0 ), IntPtr.Zero );
            if (enable) {
                control.Refresh();
            }
        }

        public static void PostMessage( Control control, uint msg, IntPtr wParam, IntPtr lParam )
        {
            PostMessage( control.Handle, msg, wParam, lParam );
        }
    }

    static class PropertiesDialog
    {
        [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Auto )]
        struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public int fMask;
            public IntPtr hWnd;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpVerb;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpFile;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpParameters;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpClass;
            public IntPtr hkeyClass;
            public int dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        const int SEE_MASK_INVOKEIDLIST = 0x000C;
        const int SEE_MASK_NOCLOSEPROCESS = 0x0040;
        const int SEE_MASK_NOASYNC = 0x0100;
        const int SEE_MASK_FLAG_NO_UI = 0x0400;

        [DllImport( "shell32.dll", CharSet = CharSet.Auto )]
        static extern bool ShellExecuteEx( ref SHELLEXECUTEINFO lpExecInfo );

        public static bool ShowDialog( string path )
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf( info );
            info.lpVerb = "properties";
            info.lpFile = path;
            info.fMask = SEE_MASK_NOCLOSEPROCESS | SEE_MASK_INVOKEIDLIST | SEE_MASK_FLAG_NO_UI | SEE_MASK_NOASYNC;

            return ShellExecuteEx( ref info );
        }
    }

    static class KeyState
    {
        [DllImport( "user32.dll" )]
        static extern short GetKeyState( Keys vKey );

        [DllImport( "user32.dll" )]
        static extern short GetAsyncKeyState( Keys vKey );

        public static bool IsPressedSync( Keys key ) { return (GetAsyncKeyState( key ) < 0); }
        public static bool IsPressedAsync( Keys key ) { return (GetAsyncKeyState( key ) < 0); }
    }
}
