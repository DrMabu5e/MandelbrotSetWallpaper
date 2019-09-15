using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.Modeling.Diagrams;

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

        public int ResolutionX { get; set; } = 4096;
        public int ResolutionY { get; set; } = 2304;

        public double StartRE { get; set; } = -1;
        public double StartIM { get; set; } = 1;

        public double EndRE { get; set; } = 0;
        public double EndIM { get; set; } = 0.4375;

        
        public double NumberOfIterationsPerMaxIter(Complex c)
        {
            
            Complex z = new Complex(0.0, 0.0);
            int iterations = 0;
            do
            {
                z = z * z + c;
                iterations += 1;
            } while (System.Numerics.Complex.Abs(z) <= 2.0 && iterations < MaxIter);
            if (iterations < MaxIter)
            {
                return (double)iterations / MaxIter;
            }
            else
            {
                return 0.0;
            }

        }

   
        public Color GetColor(double value)
        {
            const double MaxColor = 256;
            const double ContrastValue = 0.2;
            return Color.FromArgb(0, 0, (int)(MaxColor * Math.Pow(value, ContrastValue)));
        }

        public Color GetColor2(double value)
        {
            double hue = 30.0 + 255.0 * value;
            int saturation = 140;
            int luminosity = 0;
            if (value<MaxIter) { luminosity = 100; }
            
            HslColor col = new Microsoft.VisualStudio.Modeling.Diagrams.HslColor(Convert.ToInt32(hue),saturation,luminosity);
            Color rgbcol = col.ToRgbColor();

            return rgbcol;
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

                    //var c = new Complex(StartRE + (x / rx) * (EndRE - StartRE), StartIM + (y / ry) * (EndIM - StartIM));
                    //double m = NumberOfIterationsPerMaxIter(c);

                    //int color = 255 - (m * 255 / MaxIter);

                    //int color = 255 - (m * 255 / MaxIter);

                    //Color col = System.Drawing.Color.FromArgb(color);

                    //double hue = 255.0 * m / MaxIter;
                    //int sat = 1;
                    //int val = 0;
                    //if (m < MaxIter) { val = 1; }

                    //Color col = ColorFromHSV(hue, sat, val);

                    

                    double Re = StartRE + (x / rx) * (EndRE - StartRE);
                    double Im = StartIM + (y / ry) * (EndIM - StartIM);

                    Complex c = new Complex(Re, Im);

                    double m = NumberOfIterationsPerMaxIter(c);

                    //double color = 255 - (m * 255);
                   
                    //Color col = System.Drawing.Color.FromArgb(Convert.ToInt32(color));

                    Color col = GetColor2(m);

                    wallpaper.SetPixel(x, y, col);
                    
                }
            }

            wallpaper.Save("C:\\Users\\smilj\\Pictures\\Wallpaper.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    
    
}
