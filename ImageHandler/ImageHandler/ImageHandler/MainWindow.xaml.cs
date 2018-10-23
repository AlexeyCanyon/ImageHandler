using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System;

namespace ImageHandler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImageClicked(object sender, RoutedEventArgs e)
        {
            Image selectedImage = (Image) e.Source;
            sizeOfImage.Content = "Размер файла в пикселях: " + (MainImage.ActualHeight * MainImage.ActualWidth);
            resolutionOfImage.Content = "Разрешение файла: " + selectedImage.ActualWidth + "x" + selectedImage.ActualHeight;
            MainImage.Source = selectedImage.Source;
            
        }
    }
}