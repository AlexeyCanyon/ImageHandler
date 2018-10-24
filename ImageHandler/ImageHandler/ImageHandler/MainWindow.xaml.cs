using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using Image = System.Drawing.Image;
using ImageWPF = System.Windows.Controls.Image;

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
            ImageWPF selectedImage = (ImageWPF) e.Source;
            MainImage.Source = selectedImage.Source;
            Image sourceBitmap = Image.FromFile(MainImage.Source.ToString().Substring(8));
            Bitmap bmp = new Bitmap(MainImage.Source.ToString().Substring(8));
            List<string> listColors = new List<string>();
            string pixelColor;
            for (int i = 0; i < sourceBitmap.Width-1; i++)
            {
                for (int j = 0; j < sourceBitmap.Height-1; j++)
                {
                    pixelColor = ColorTranslator.ToHtml(bmp.GetPixel(i,j));
                    if (listColors.IndexOf(pixelColor) == -1)
                    {
                        listColors.Add(pixelColor);
                    }
                    
                }
            }
            
             //x-в пикселях , y - в пикселях
            currentPixel.Content = "Различных цветов в картинке: " + listColors.Count;
            
            
            sizeOfImage.Content = "Размер файла в пикселях: " + (sourceBitmap.Height * sourceBitmap.Width);
            resolutionOfImage.Content = "Разрешение файла: " + sourceBitmap.Width + "x" + sourceBitmap.Height;
        }
        
        private void AddPictures(object sender, RoutedEventArgs e)
        {
        }
    }
}