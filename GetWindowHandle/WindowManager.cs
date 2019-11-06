using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace GetWindowHandle
{
    public class WindowManager
    {
        delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static IEnumerable<IntPtr> EnumWindows(int processId)
        {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
                EnumThreadWindows(thread.Id, 
                    (hWnd, lParam) => { handles.Add(hWnd); return true; }, 
                    IntPtr.Zero);

            return handles;
        }

        public IEnumerable<IntPtr> GetAllWindowHandles(string procName) =>
            Process.GetProcessesByName(procName)
                .SelectMany(p => EnumWindows(p.Id));

        public bool MinimizeWindow(IntPtr h) => ShowWindow(h, 6);
    }
}
