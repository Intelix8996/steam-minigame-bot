using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace SteamAim
{
    public partial class Form1 : Form
    {
        static Size ScreenSize;
        static Bitmap bmp;
        static bool Active;
        static System.Windows.Forms.Timer Update_Timer, Reload_Timer;
        static Point LevelPosition;
        static Point ResetButtonOffset;

        static Label InternalStatusLabel;

        static float ScreenWidth, ScreenHeight;

        static float ScreenRatio, ReferenceRatio;

        public Form1()
        {
            InitializeComponent();

            Update_Timer = new System.Windows.Forms.Timer();
            Reload_Timer = new System.Windows.Forms.Timer();

            Update_Timer.Tick += Update_Timer_Tick;
            Reload_Timer.Tick += Reload_Timer_Tick;
            Update_Timer.Interval = 450;
            Reload_Timer.Interval = 135000;

            Reload_Timer.Stop();
            Update_Timer.Stop();

            InternalStatusLabel = StatusLabel;

            ScreenWidth = Screen.PrimaryScreen.Bounds.Size.Width;
            ScreenHeight = Screen.PrimaryScreen.Bounds.Size.Height;

            ScreenRatio = ScreenWidth / ScreenHeight;
            ReferenceRatio = (float)16 / (float)10;

            if (ScreenRatio == ReferenceRatio)
                ResetButtonOffset = new Point(0, -45);

            LevelPosition = new Point(Convert.ToInt32(ScreenWidth) / 2, Convert.ToInt32(ScreenHeight) / 2);
        }

        private void Reload_Timer_Tick(object sender, EventArgs e)
        {
            Cursor.Position = Add(new Point(Convert.ToInt32(ScreenWidth) / 2, Convert.ToInt32(ScreenHeight) / 2), ResetButtonOffset);

            Thread.Sleep(2500);

            Cursor.Position = LevelPosition;
        }

        static void OnKeyDown()
        {
            Active = !Active;

            if (Active)
            {
                InternalStatusLabel.Text = "Status: Running";

                Update_Timer.Start();
                Reload_Timer.Start();

                UnHook();

                LevelPosition = Cursor.Position;

                SendKeys.SendWait("{F6}");

                SetHook();
            }
            else
            {
                InternalStatusLabel.Text = "Status: Stopped";

                Update_Timer.Stop();
                Reload_Timer.Stop();

                UnHook();
                SendKeys.SendWait("{F6}");
                SetHook();
            }
        }

        void Update_Timer_Tick(Object myObject, EventArgs myEventArgs)
        {
            try
            {
                ScreenSize = Screen.PrimaryScreen.Bounds.Size;
                bmp = new Bitmap(ScreenSize.Width, ScreenSize.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, ScreenSize);
                }
            }
            catch { }

            for (int i = 0; i < bmp.Width; i += 5)
            {
                for (int j = 0; j < bmp.Height; j += 5)
                {
                    Color col1 = bmp.GetPixel(i, j);
                    Color[] col_ref = new Color[2];

                    col_ref[0] = Color.FromArgb(39, 141, 220);
                    col_ref[1] = Color.FromArgb(64, 137, 204);

                    float hue = col1.GetHue();
                    float[] hue_ref = new float[2];

                    for (int m = 0; m < col_ref.Length; ++m)
                    {
                        hue_ref[m] = col_ref[m].GetHue();

                        if (hue == hue_ref[m])
                        {
                            Cursor.Position = new Point(i - 75, j);

                            SendKeys.SendWait("%{1}");
                            SendKeys.SendWait("%{3}");
                            SendKeys.SendWait("%{4}");
                            SendKeys.SendWait("%{5}");

                            for (int delay = 0; delay < 1000000; ++delay) { }
                        }
                    }
                }
            }

            StatusLabel = InternalStatusLabel;
        }


        //обработка глобального нажатия f5
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);


        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13; 
        const int WM_KEYDOWN = 0x100; 

        private static LowLevelKeyboardProc _proc = hookProc;

        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                Keys keyCode = (Keys)Marshal.ReadInt32(lParam);

                if (keyCode == Keys.F5)
                    OnKeyDown();

                return (IntPtr)1;
            }
            else
                return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }


        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }

        private static IntPtr hhook = IntPtr.Zero;

        public static void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetHook();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnHook();
        }

        static Point Add(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    }
}
