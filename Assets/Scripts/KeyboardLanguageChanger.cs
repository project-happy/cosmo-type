using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Assets.Scripts
{
    public class KeyboardLanguageChanger
    {
        // Define the language identifier for the desired keyboard language (e.g., "0000040D" for Hebrew , "00000409" for English - United States)
        private readonly static Dictionary<string, string> LanguageIdentifiers = new Dictionary<string, string>()
        {
            { "en", "00000409"},
            { "he", "0000040D"},
        };

        // Import the necessary functions from user32.dll (Windows) or libx11 (Linux/macOS)
//#if UNITY_STANDALONE_WIN
        [DllImport("user32.dll")]
        private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        [DllImport("user32.dll")]
        private static extern bool ActivateKeyboardLayout(IntPtr hkl, uint Flags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);
//#elif UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
//        [DllImport("libx11")]
//        private static extern IntPtr XOpenDisplay(string display_name);

//        [DllImport("libx11")]
//        private static extern IntPtr XSetLocaleModifiers(string modifiers);

//        [DllImport("libx11")]
//        private static extern IntPtr XCreateSimpleWindow(IntPtr display, IntPtr parent, int x, int y, uint width, uint height, uint border_width, uint border, uint background);

//        [DllImport("libx11")]
//        private static extern void XMapWindow(IntPtr display, IntPtr w);

//        [DllImport("libx11")]
//        private static extern IntPtr XStoreName(IntPtr display, IntPtr w, string window_name);

//        [DllImport("libx11")]
//        private static extern void XCloseDisplay(IntPtr display);

//        [DllImport("libx11")]
//        private static extern int XSetLocaleModifiers(string modifiers);

//        [DllImport("libx11")]
//        private static extern IntPtr XCreateSimpleWindow(IntPtr display, IntPtr parent, int x, int y, uint width, uint height, uint border_width, uint border, uint background);
//#endif

        public static void ChangeKeyboardLanguage(string identifier = "en")
        {
//#if UNITY_STANDALONE_WIN
            // Load the keyboard layout for the specified language code
            IntPtr hkl = LoadKeyboardLayout(LanguageIdentifiers.GetValueOrDefault(identifier ?? "en"), 1);

            // Activate the new keyboard layout
            ActivateKeyboardLayout(hkl, 0);
//#elif UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
//            // Open the X11 display
//            IntPtr display = XOpenDisplay(null);

//            // Set the locale modifiers to the specified language code
//            XSetLocaleModifiers(languageCode);

//            // Create a simple window to set the input focus
//            IntPtr window = XCreateSimpleWindow(display, IntPtr.Zero, 0, 0, 1, 1, 0, 0, 0);

//            // Set the window name
//            XStoreName(display, window, "Unity");

//            // Map the window to make it visible
//            XMapWindow(display, window);

//            // Close the display
//            XCloseDisplay(display);
//#endif
        }
    }
}
