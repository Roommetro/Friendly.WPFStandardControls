using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static RM.Friendly.WPFStandardControls.Inside.NativeMethods;

namespace RM.Friendly.WPFStandardControls.Inside
{
    internal static class SendInputEx
    {
        /// <summary>
        /// Send key.
        /// </summary>
        internal static void SendKey(Keys key)
        {
            KeyDown(key);
            KeyUp(key);
        }

        /// <summary>
        /// Key down.
        /// </summary>
        /// <param name="key">key.</param>
        static void KeyDown(Keys key)
        {
            WaitForTimerMessage();
            KeyboardInput(KeyboardStroke.KEY_DOWN | KeyboardStroke.KEYEVENTF_EXTENDEDKEY, key);
            WaitForTimerMessage();
        }

        /// <summary>
        /// Key up.
        /// </summary>
        /// <param name="key">key.</param>
        static void KeyUp(Keys key)
        {
            WaitForTimerMessage();
            KeyboardInput(KeyboardStroke.KEY_UP | KeyboardStroke.KEYEVENTF_EXTENDEDKEY, key);
            WaitForTimerMessage();
        }

        static void KeyboardInput(KeyboardStroke flags, Keys key)
        {
            int keyboardFlags = (int)flags | 0x0004; //KBD_UNICODE = 0x0004
            short virtualKey = (short)key;
            short scanCode = (short)MapVirtualKey(virtualKey, 0);

            Input input = new Input();
            input.Type = 1; // KEYBOARD = 1
            input.U.Keyboard.Flags = keyboardFlags;
            input.U.Keyboard.VirtualKey = virtualKey;
            input.U.Keyboard.ScanCode = scanCode;
            input.U.Keyboard.Time = 0;
            input.U.Keyboard.ExtraInfo = IntPtr.Zero;

            var inputArray = new[] { input };
            SendInput(inputArray.Length, inputArray, Marshal.SizeOf(typeof(Input)));
            GC.KeepAlive(inputArray);
        }

        static void WaitForTimerMessage()
        {
            var timer = new TimerMessageWaiter();
            while (!timer.Arrived)
            {
                Application.DoEvents();
                InvokeUtility.DoEvents();
            }
        }

        class TimerMessageWaiter
        {
            internal bool Arrived { get; set; }
            internal TimerMessageWaiter()
            {
                var timer = new Timer { Interval = 1 };
                timer.Tick += (_, __) =>
                {
                    timer.Stop();
                    Arrived = true;
                };
                timer.Start();
            }
        }
    }
}
