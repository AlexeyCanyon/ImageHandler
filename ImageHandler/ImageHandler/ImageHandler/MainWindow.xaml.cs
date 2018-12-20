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
using System.Collections.Concurrent;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ImageHandler
{

    public partial class MainWindow
    {
        Picture[] pictures;
        Picture mainPicture;

        public MainWindow()
        {
            pictures = Database.GetPictures();
            InitializeComponent();

            foreach (Picture picture in pictures)
            {
                ImageWPF image = new ImageWPF();
                BitmapImage bi31 = new BitmapImage();
                bi31.BeginInit();
                bi31.UriSource = new Uri(picture.File);
                bi31.EndInit();
                image.Source = bi31;
                image.Width = 350;
                image.HorizontalAlignment = HorizontalAlignment.Center;
                imagesPanel.Children.Add(image);
            }
            mainPicture = pictures[0];
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(mainPicture.File);
            bi3.EndInit();
            MainImage.Source = bi3;
            UpdateMetaData();
        }

        private void ImageClicked(object sender, RoutedEventArgs e)
        {
            ImageWPF selectedImage = (ImageWPF) e.Source;
            MainImage.Source = selectedImage.Source;
            string selectImagePath = MainImage.Source.ToString().Substring(8);
            Image sourceBitmap = Image.FromFile(selectImagePath);

            int lenght = selectImagePath.Length;
            ResizeImage(selectImagePath, selectImagePath.Insert(lenght - 4, "-Mini"), 64, 64, false);

            ConcurrentBag<ulong> redCol = new ConcurrentBag<ulong>();
            ConcurrentBag<ulong> greenCol = new ConcurrentBag<ulong>();
            ConcurrentBag<ulong> blueCol = new ConcurrentBag<ulong>();

            Bitmap bmp = new Bitmap(selectImagePath);
            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bmp.Height;
                int widthInBytes = bmp.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        ulong oldBlue = currentLine[x];
                        ulong oldGreen = currentLine[x + 1];
                        ulong oldRed = currentLine[x + 2];

                        redCol.Add(oldRed);
                        greenCol.Add(oldGreen);
                        blueCol.Add(oldBlue);
                    }
                });
                bmp.UnlockBits(bitmapData);
            }

            ulong[] rA = new ulong[redCol.Count];
            ulong[] gA = new ulong[greenCol.Count];
            ulong[] bA = new ulong[blueCol.Count];

            float r = 0;
            float g = 0;
            float b = 0;

            redCol.CopyTo(rA, 0);
            greenCol.CopyTo(gA, 0);
            blueCol.CopyTo(bA, 0);
            for (int i = 0; i < rA.Length; i++)
            {
                r += rA[i];
                g += gA[i];
                b += bA[i];
            }

            double procentRed = r / (sourceBitmap.Height * sourceBitmap.Width);
            procentRed = Math.Round(procentRed / 256 * 100, 1);
            double procentGreen = g / (sourceBitmap.Height * sourceBitmap.Width);
            procentGreen = Math.Round(procentGreen / 256 * 100, 1);
            double procentBlue = b / (sourceBitmap.Height * sourceBitmap.Width);
            procentBlue = Math.Round(procentBlue / 256 * 100, 1);

            PercentOfRedLabel.Content = "Насыщенность красного: " + procentRed + "%";
            PercentOfGreenLabel.Content = "Насыщенность зеленого: " + procentGreen + "%";
            PercentOfBlueLabel.Content = "Насыщенность синего: " + procentBlue + "%";
            sizeOfImage.Content = "Размер файла в пикселях: " + (sourceBitmap.Height * sourceBitmap.Width);
            resolutionOfImage.Content = "Разрешение файла: " + sourceBitmap.Width + "x" + sourceBitmap.Height;

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
                YearOfCreation = "1900",
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
                YearOfCreation = "900",
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
                YearOfCreation = "100",
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

        private void ChangePicture(object sender, RoutedEventArgs e)
        {
            ChangePictureWindow window = new ChangePictureWindow(mainPicture);
            window.Show();
            UpdateMetaData();
        }

        private void ResizeImage(string OrigFile, string NewFile, int NewWidth, int MaxHeight, bool ResizeIfWider)
        {
            System.Drawing.Image FullSizeImage = System.Drawing.Image.FromFile(OrigFile);
            // Ensure the generated thumbnail is not being used by rotating it 360 degrees
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (ResizeIfWider)
            {
                if (FullSizeImage.Width <= NewWidth)
                {
                    NewWidth = FullSizeImage.Width;
                }
            }

            int NewHeight = FullSizeImage.Height * NewWidth / FullSizeImage.Width;
            if (NewHeight > MaxHeight) // Height resize if necessary
            {
                NewWidth = FullSizeImage.Width * MaxHeight / FullSizeImage.Height;
                NewHeight = MaxHeight;
            }

            // Create the new image with the sizes we've calculated
            System.Drawing.Image NewImage = FullSizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);
            FullSizeImage.Dispose();
            NewImage.Save(NewFile);
        }

        private void FindFace(string selectImagePath)
        {
            IImage image;
            //            image = new UMat(selectImagePath, ImreadModes.Color); //UMat version
            image = new Mat(selectImagePath, ImreadModes.Color); //CPU version

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

        private void UpdateMetaData()
        {
            NameLabel.Content = "Название картины: " + mainPicture.Name;
            AuthorLabel.Content = "Автор картины: " + mainPicture.Author;
            YearOfCreationLabel.Content = "Дата создания: " + mainPicture.YearOfCreation;
            MaterialLabel.Content = "Материал: " + mainPicture.Material;
            DescriptionLabel.Content = "Описание: " + mainPicture.Description;
            RulesLabel.Content = "Правила использования: " + mainPicture.Rules;
            TechnologLabel.Content = "Технология: " + mainPicture.Technology;
            PercentOfRedLabel.Content = "Насыщенность красного: " + mainPicture.PercentOfRed + "%";
            PercentOfGreenLabel.Content = "Насыщенность зеленого: " + mainPicture.PercentOfGreen + "%";
            PercentOfBlueLabel.Content = "Насыщенность синего: " + mainPicture.PercentOfBlue + "%";
            PlaceOfStorageLabel.Content = "Место хранения: " + mainPicture.PlaceOfStorage;
            PlaceOfCreationLabel.Content = "Место создания: " + mainPicture.PlaceOfCreation;
        }

    }
}