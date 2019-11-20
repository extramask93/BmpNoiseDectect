using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using steg_2.lib;

namespace steg_2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap source;
        Bitmap destination;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void load_image_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

                source = new Bitmap(openFileDialog.FileName);
                source_image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                           source.GetHbitmap(),
                                           IntPtr.Zero,
                                           System.Windows.Int32Rect.Empty,
                                           BitmapSizeOptions.FromWidthAndHeight(source.Width, source.Height));
            }
        }

        private void save_image_button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                destination.Save(dialog.FileName, ImageFormat.Bmp);
            }
        }


        private void noise_button_Click(object sender, RoutedEventArgs e)
        {
            BmpTransform bt = new BmpTransform(source);
            var nb = bt.markedBitmap();
            destination_image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                           nb.GetHbitmap(),
                                           IntPtr.Zero,
                                           System.Windows.Int32Rect.Empty,
                                           BitmapSizeOptions.FromWidthAndHeight(nb.Width, nb.Height));
            var total =  bt.nrOFPixelsChanged()*100/(nb.Width*nb.Height);
            cLabel.Content = total.ToString() + " %";
        }
    }
}
