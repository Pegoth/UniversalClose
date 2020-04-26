using System;
using System.Diagnostics;

namespace UniversalClose
{
    public static class DebugLogger
    {
        [Conditional("DEBUG")]
        public static void Print(string format, params object[] args) => Debug.Print($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]: {string.Format(format, args)}");

        [Conditional("DEBUG")]
        public static void Print(string str) => Debug.Print($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]: {str}");
    }
}