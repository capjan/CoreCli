using System.Runtime.InteropServices;

namespace UptimeExe {
    public static class SafeNativeMethods
    {
        [DllImport("kernel32")]
        public static extern ulong GetTickCount64();

        [DllImport("kernel32")]
        public static extern uint GetTickCount();
    }
}