using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Microsoft.VisualStudio.Modeling.Diagrams;


namespace Mandelbrot_Set_Wallpaper
{
    public class MandelbrotSet
    {
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
        //maxIter = (value > 0 && value <= 2000) ? value :  80; 
        public int ResolutionX { get; set; } = 4096;
        public int ResolutionY { get; set; } = 2304;

        public double StartRE { get; set; } = -1;
        public double StartIM { get; set; } = 1;

        public double EndRE { get; set; } = 0;
        public double EndIM { get; set; } = 0.4375;

        public string ColorMode { get; set; } = "RGB";


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

        public Color GetColorRGB(double value)
        {
            const double MaxColor = 256;
            const double ContrastValue = 0.2;
            return Color.FromArgb(0, 0, (int)(MaxColor * Math.Pow(value, ContrastValue)));
        }

        public Color GetColorHSV(double value)
        {
            double hue = 30.0 + 255.0 * value;
            int saturation = 140;
            int luminosity = 0;
            if (value<MaxIter) { luminosity = 100; }
            
            HslColor col = new Microsoft.VisualStudio.Modeling.Diagrams.HslColor(Convert.ToInt32(hue),saturation,luminosity);
            Color rgbcol = col.ToRgbColor();

            return rgbcol;
        }

        public BitmapImage PlotPreview()
        {
            int previewX = 400;
            int previewY = 225;
            Bitmap preview = new Bitmap(previewX, previewY);
            BitmapImage previewImage = new BitmapImage();
            
            double rx = Convert.ToDouble(previewX);
            double ry = Convert.ToDouble(previewY);

            MaxIter = 80;

            for (int x = 0; x < previewX; x++)
            {
                for (int y = 0; y < previewY; y++)
                {
                    double Re = StartRE + (x / rx) * (EndRE - StartRE);
                    double Im = StartIM + (y / ry) * (EndIM - StartIM);

                    Complex c = new Complex(Re, Im);

                    double m = NumberOfIterationsPerMaxIter(c);

                    Color col = new Color();

                    switch (ColorMode)
                    {
                        case "RGB":
                            col = GetColorRGB(m);
                            break;
                        case "HSV":
                            col = GetColorHSV(m);
                            break;
                    }

                    preview.SetPixel(x, y, col);
                }
            }

            MemoryStream ms = new MemoryStream();

            preview.Save(ms, ImageFormat.Png);
            ms.Position = 0;

            previewImage.BeginInit();
            previewImage.StreamSource = ms;
            previewImage.CacheOption = BitmapCacheOption.OnLoad;
            previewImage.EndInit();

            previewImage.Freeze();

            return previewImage;  
        }


        public void plot()
        {
            Bitmap wallpaper = new Bitmap(ResolutionX, ResolutionY);
            double rx = Convert.ToDouble(ResolutionX);
            double ry = Convert.ToDouble(ResolutionY);

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.FileName = "Wallpaper";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files (*.png)|*.png|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                for (int x = 0; x < ResolutionX; x++)
                {
                    for (int y = 0; y < ResolutionY; y++)
                    {
                        double Re = StartRE + (x / rx) * (EndRE - StartRE);
                        double Im = StartIM + (y / ry) * (EndIM - StartIM);

                        Complex c = new Complex(Re, Im);

                        double m = NumberOfIterationsPerMaxIter(c);

                        Color col = new Color();

                        switch (ColorMode)
                        {
                            case "RGB":
                                col = GetColorRGB(m);
                                break;
                            case "HSV":
                                col = GetColorHSV(m);
                                break;
                        }
                        wallpaper.SetPixel(x, y, col);
                    }
                }
                wallpaper.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    } 
}
