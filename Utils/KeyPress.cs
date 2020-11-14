using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LichtknarrePlugin.Utils
{
    public class HotKeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int key;
        private IntPtr hWnd;
        private int id;

        public HotKeyHandler(Keys key)
        {
            this.key = (int)key;
            this.hWnd = (IntPtr)NativeWin32.FindWindow(null, "Skeleton Basics");
            id = this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, 0, key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(hWnd, id);
        }
    }

    public class PressKey
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        public Keys key;
        public bool up = false;
        public int steps = 0;
        public int sleepBeforeTime = 0;

        private bool holdStart = false;
        private double holdStartTime = 0;
        private bool sleepBefore = false;
        private double ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }

        public void Press()
        {

            double now = ConvertToTimestamp(DateTime.Now);

            try
            {
                if (this.sleepBeforeTime != 0)
                {
                    System.Threading.Thread.Sleep(this.sleepBeforeTime);
                }

                keybd_event((byte)key, 0, KEY_DOWN_EVENT, 0);
                System.Threading.Thread.Sleep(steps);
                keybd_event((byte)key, 0, KEY_UP_EVENT, 0);
            }
            finally
            {

            }
        }

        public void hold()
        {
            try
            {
                if (this.sleepBeforeTime != 0)
                {
                    System.Threading.Thread.Sleep(this.sleepBeforeTime);
                }
                keybd_event((byte)key, 0, KEY_DOWN_EVENT, 0);
            }
            finally
            {

            }
        }

        public void release()
        {
            try
            {
                if (this.sleepBeforeTime != 0)
                {
                    System.Threading.Thread.Sleep(this.sleepBeforeTime);
                }
                keybd_event((byte)key, 0, KEY_UP_EVENT, 0);
            }
            finally
            {

            }
        }
    }
}
