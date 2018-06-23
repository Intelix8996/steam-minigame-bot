using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace SteamAim
{
    public partial class Form1 : Form
    {
        static Size ScreenSize;
        static Bitmap bmp;

        public Form1()
        {
            InitializeComponent();

            Timer timer = new Timer();

            timer.Tick += new EventHandler(OnTick);

            timer.Interval = 350;
            timer.Start();
        }

        private static void OnTick(Object myObject, EventArgs myEventArgs)
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

            for (int i = 0; i < bmp.Height; i += 5)
            {
                for (int j = 0; j < bmp.Width; j += 5)
                {
                    Color col1 = bmp.GetPixel(j, i);
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
                            Cursor.Position = new Point(j - 75, i);

                            SendKeys.SendWait("{1}");
                            SendKeys.SendWait("{3}");
                            SendKeys.SendWait("{4}");
                            SendKeys.SendWait("{5}");
                        }
                    }
                }
            }
        }
    }
}
