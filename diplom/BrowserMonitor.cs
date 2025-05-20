using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace diplom
{
    public static class BrowserMonitor
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, buff, nChars) > 0)
            {
                return buff.ToString();
            }

            return "Без назви";
        }
        public static bool IsBrowserActive()
        {
            string[] browsers = { "Chrome", "Edge", "Firefox", "Opera", "Brave" };
            string activeWindowTitle = GetActiveWindowTitle();

            return browsers.Any(browser => activeWindowTitle.Contains(browser));
        }
    }
}







