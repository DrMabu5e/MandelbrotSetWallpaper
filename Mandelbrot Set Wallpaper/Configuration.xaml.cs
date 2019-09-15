using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mandelbrot_Set_Wallpaper
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Page
    {
        public Configuration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MandelbrotSet mandl = new MandelbrotSet();

            mandl.MaxIter = Int32.Parse(MaxIt.Text);
            mandl.ResolutionX = Int32.Parse(ResX.Text);
            mandl.ResolutionY = Int32.Parse(ResY.Text);
            mandl.StartRE = Convert.ToDouble(StRe.Text);
            mandl.StartIM = Convert.ToDouble(StIm.Text);
            mandl.EndRE = Convert.ToDouble(EnRe.Text);
            mandl.EndIM = Convert.ToDouble(EnIm.Text);
            PreviewWindow.Source = mandl.PlotPreview();


        }
    }
}
