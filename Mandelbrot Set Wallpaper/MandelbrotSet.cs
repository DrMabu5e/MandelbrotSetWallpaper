using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Drawing.Imaging;

namespace Mandelbrot_Set_Wallpaper
{
    public class MandelbrotSet
    {
        ///private int maxIter;
        ///private int resolutionX;
        ///private int resolutionY;
        ///private int startRE;
        ///private int startIM;
        ///private int endRE;
        ///private int endIM;

        public int MaxIter
        {
            get { return maxIter; }
            set {
                if (value > 0 && value <= 2000)
                    maxIter = value;
                else
                    maxIter = 80;
                }
        }
        private int maxIter;

        public int ResolutionX { get; set; } = 2560;
        public int ResolutionY { get; set; } = 1440;

        public double StartRE { get; set; } = -3;
        public double StartIM { get; set; } = -1.125;

        public double EndRE { get; set; } = 1;
        public double EndIM { get; set; } = 1.125;

        
        public double NumberOfIterations(Complex c)
        {
            Complex z = new Complex(0.0, 0.0);
            double n = 0;

            while (System.Numerics.Complex.Abs(z) <= 2 && n < MaxIter)
            {
                z = z * z + c;
                n+=1;
            }
            if (n == MaxIter)
            {
                return MaxIter;
            }
            else
            {
                double log2ofz = System.Math.Log(System.Numerics.Complex.Abs(z)) / System.Math.Log(2);
                double result = n + 1.0 - System.Math.Log(log2ofz);
                return result;
            }

        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public void plot()
        {
            Bitmap wallpaper = new Bitmap(ResolutionX, ResolutionY);
            double rx = Convert.ToDouble(ResolutionX);
            double ry = Convert.ToDouble(ResolutionY);




            for (int x = 0; x < ResolutionX; x++)
            {
                for (int y = 0; y < ResolutionY; y++)
                {
                    //var c = new Complex(StartRE + (x / ResolutionX) * (EndRE - StartRE), StartIM + (y / ResolutionY) * (EndIM - StartIM));

                    var c = new Complex(StartRE + (x / rx) * (EndRE - StartRE), StartIM + (y / ry) * (EndIM - StartIM));
                    double m = NumberOfIterations(c);

                    //int color = 255 - (m * 255 / MaxIter);

                    //Color col = System.Drawing.Color.FromArgb(color);

                    double hue = 255.0 * m / MaxIter;
                    int sat = 1;
                    int val = 0;
                    if (m < MaxIter) { val = 1; }

                    Color col = ColorFromHSV(hue, sat, val);

                    wallpaper.SetPixel(x, y, col);
                    
                }
            }

            wallpaper.Save("C:\\Users\\smilj\\Pictures\\WallpaperHUE.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    
    
}
