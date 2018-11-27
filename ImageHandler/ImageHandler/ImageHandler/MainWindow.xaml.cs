using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Image = System.Drawing.Image;
using ImageWPF = System.Windows.Controls.Image;
using MessageBox = System.Windows.MessageBox;
using Newtonsoft.Json;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Cvb;
using Emgu.CV.UI;
using Emgu.CV.Cuda;


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
//            Bitmap bmp = new Bitmap(MainImage.Source.ToString().Substring(8));
//            List<string> listColors = new List<string>();
//            string pixelColor;
//            for (int i = 0; i < sourceBitmap.Width-1; i++)
//            {
//                for (int j = 0; j < sourceBitmap.Height-1; j++)
//                {
//                    pixelColor = ColorTranslator.ToHtml(bmp.GetPixel(i,j));
//                    if (listColors.IndexOf(pixelColor) == -1)
//                    {
//                        listColors.Add(pixelColor);
//                    }
//                    
//                }
//            }
            
             //x-в пикселях , y - в пикселях
//            colorsCount.Content = "Различных цветов в картинке: " + listColors.Count;
            
            sizeOfImage.Content = "Размер файла в пикселях: " + (sourceBitmap.Height * sourceBitmap.Width);
            resolutionOfImage.Content = "Разрешение файла: " + sourceBitmap.Width + "x" + sourceBitmap.Height;
            
            IImage image;
//            image = new UMat(MainImage.Source.ToString().Substring(8), ImreadModes.Color); //UMat version
            image = new Mat(MainImage.Source.ToString().Substring(8), ImreadModes.Color); //CPU version

            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();

            DetectFace.Detect(
                image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml", 
                faces, eyes,
                out detectionTime);

            foreach (Rectangle face in faces)
                CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
            foreach (Rectangle eye in eyes)
                CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);

            //display the image 
            using (InputArray iaImage = image.GetInputArray())
                ImageViewer.Show(image, String.Format(
                    "Completed face and eye detection using {0} in {1} milliseconds", 
                    (iaImage.Kind == InputArray.Type.CudaGpuMat && CudaInvoke.HasCuda) ? "CUDA" :
                    (iaImage.IsUMat && CvInvoke.UseOpenCL) ? "OpenCL" 
                    : "CPU",
                    detectionTime));

            
        }
        
        private void AddPictures(object sender, RoutedEventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter =
                "Images (*.BMP;*.JPG)|*.BMP;*.JPG";

            openFileDialog1.Multiselect = true;
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
                foreach (String file in openFileDialog1.FileNames)
                {
                    ImageWPF image = new ImageWPF();
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(file);
                    bi3.EndInit();
                    image.Source = bi3;
                    image.Width = 350;
                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    imagesPanel.Children.Add(image);
                }
        }

        private void updatePixel(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(MainImage);
            Bitmap bmp = new Bitmap(MainImage.Source.ToString().Substring(8));
            currentPixel.Content = "Код текущего пикселя: " + ColorTranslator.ToHtml(bmp.GetPixel((int) Math.Round (p.X), (int) Math.Round (p.Y)));
        }

        private void OpenMap(object sender, RoutedEventArgs e)
        {
            Picture[] mas = new Picture[3];
            mas[0] = new Picture()
            {
                Name = "Вася",
                Author = "Пушкин",
                YearOfCreation = 1900,
                Longitude = 20.3f,
                Latitude = 50.5f,
                LongitudeCreation = 100f,
                LatitudeCreation = 10f,
                LongitudeStorage = 20.3f,
                LatitudeStorage = 50.5f,
                Height = 50,
                Width = 200,
                PercentOfRed = 30f,
                PercentOfGreen = 70f,
                PercentOfBlue = 0f,
                Rules = "Лол кек"
            };

            mas[1] = new Picture()
            {
                Name = "Коля",
                Author = "Hideo Kojima",
                YearOfCreation = 900,
                Longitude = 28.3f,
                Latitude = 52.5f,
                LongitudeCreation = 10f,
                LatitudeCreation = 17f,
                LongitudeStorage = 28.3f,
                LatitudeStorage = 52.5f,
                Height = 70,
                Width = 290,
                PercentOfRed = 50f,
                PercentOfGreen = 20f,
                PercentOfBlue = 30f,
                Rules = "Лол кек чабурек"
            };

            mas[2] = new Picture()
            {
                Name = "Корней",
                Author = "Эх",
                YearOfCreation = 100,
                Longitude = 50.3f,
                Latitude = 58.5f,
                LongitudeCreation = 85f,
                LatitudeCreation = 32f,
                LongitudeStorage = 50.3f,
                LatitudeStorage = 58.5f,
                Height = 8000,
                Width = 120,
                PercentOfRed = 10f,
                PercentOfGreen = 20f,
                PercentOfBlue = 70f,
                Rules = "Лол кек"
            };

            string serialized = JsonConvert.SerializeObject(mas);
            using (FileStream fstream = new FileStream(@"note.js", FileMode.Create))
            {
                byte[] array = System.Text.Encoding.UTF8.GetBytes("var picturesFile = '" + serialized + "';");
                fstream.Write(array, 0, array.Length);
            }
            MapWindow map = new MapWindow();
            map.Show();
        }
        
    }
}