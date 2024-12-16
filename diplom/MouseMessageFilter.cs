using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace diplom
{
    // Клас для моніторингу миші
    public class GlobalHooks
    {
        // Визначення необхідних API для клавіатурних і мишачих хуків
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_MOUSE_LL = 14; // Ловить події миші
        private const int WH_KEYBOARD_LL = 13; // Ловити події клавіатури

        private static IntPtr mouseHook = IntPtr.Zero;
        private static IntPtr keyboardHook = IntPtr.Zero;

        public static void Start()
        {
            mouseHook = SetWindowsHookEx(WH_MOUSE_LL, MouseProc, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
            keyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardProc, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
        }

        public static void Stop()
        {
            UnhookWindowsHookEx(mouseHook);
            UnhookWindowsHookEx(keyboardHook);
        }

        private static int MouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // Обробка руху миші
                Console.WriteLine("Рух миші в глобальному просторі");
            }
            return CallNextHookEx(mouseHook, nCode, wParam, lParam);
        }

        private static int KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // Обробка натискання клавіші
                Console.WriteLine("Натискання клавіші в глобальному просторі");
            }
            return CallNextHookEx(keyboardHook, nCode, wParam, lParam);
        }
    }

}
