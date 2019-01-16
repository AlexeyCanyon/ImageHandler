using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
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
using System.Net;
using System.Windows.Controls;

namespace ImageHandler
{

    public partial class MainWindow
    {
        Picture[] pictures;
        int mainPictureNum;
        static string DataPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";
        static string DataMiniPath = AppDomain.CurrentDomain.BaseDirectory + "DataMini\\";
        static string DataMap = AppDomain.CurrentDomain.BaseDirectory + "DataMap\\";
        static string[] filePaths = Directory.GetFiles(DataPath);
        static string[] fileMiniPaths = Directory.GetFiles(DataMiniPath);
        static string[] fileMap = Directory.GetFiles(DataMap);

        public MainWindow()
        {
            pictures = Database.GetPictures();
            if(pictures.Length == 0)
            {
                this.Close();
                return;
            }
            InitializeComponent();
            
            if (filePaths.Length != pictures.Length)
            {
                WebClient client = new WebClient();
               
                for (int i = 0, j = 0; i < pictures.Length; i++)
                {
                    bool searchID = false;
                    for (j = 0; j < filePaths.Length; j++)
                    {
                        if (Path.GetFileNameWithoutExtension(filePaths[j]) == pictures[i].ID)
                        {
                            searchID = true;
                            break;
                        }
                    }
                    if (!searchID)
                    {
                        try
                        {
                            
                            BitmapImage bi31 = new BitmapImage();
                            bi31.BeginInit();
                            bi31.UriSource = new Uri(pictures[i].File);
                            client.DownloadFile(bi31.UriSource, DataPath + pictures[i].ID + ".jpg");
                            bi31.EndInit();
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Ошибка загрузки картины " + pictures[i].ID +  " из базы: " + e.Message);
                        }
                    }
                    
                }
                
                client.Dispose();
            }

            filePaths = Directory.GetFiles(DataPath);
            if (!(Directory.GetFiles(DataMiniPath).Length == filePaths.Length) || !(Directory.GetFiles(DataMap).Length == filePaths.Length))
            {
                ProgressBarWindow pb = new ProgressBarWindow();
                pb.Show();
                pb.Title = "Генерация миниатюр";
                for (int i = 0; i < filePaths.Length; i++)
                {

                    if (filePaths[i] != fileMiniPaths[i])
                        ResizeImage(filePaths[i], DataMiniPath + Path.GetFileName(filePaths[i]), 350, 350, false);
                    if (filePaths[i] != fileMap[i])
                        ResizeImage(filePaths[i], DataMap + Path.GetFileName(filePaths[i]), 64, 64, false);
                    fileMiniPaths = Directory.GetFiles(DataMiniPath);
                    fileMap = Directory.GetFiles(DataMap);
                    if ((Directory.GetFiles(DataMiniPath).Length == filePaths.Length) && (Directory.GetFiles(DataMap).Length == filePaths.Length))
                    {
                        break;
                    }
                    pb.pbStatus.Value = i / pictures.Length * 100;
                }
                pb.Close();
            }
           

            for (int i = 0; i < filePaths.Length; i++)
            {
                BitmapImage bi31 = new BitmapImage();
                bi31.BeginInit();
                fileMiniPaths = Directory.GetFiles(DataMiniPath);
                bi31.UriSource = new Uri(fileMiniPaths[i]);
                bi31.EndInit();
                ImageWPF image = new ImageWPF();
                image.Name = "id" + Path.GetFileNameWithoutExtension(filePaths[i]);
                image.Source = bi31;
                image.HorizontalAlignment = HorizontalAlignment.Center;

                Grid grid = new Grid();
                ColumnDefinition colDef1 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef1);
                RowDefinition rowDef1 = new RowDefinition();
                RowDefinition rowDef2 = new RowDefinition();
                grid.RowDefinitions.Add(rowDef1);
                grid.RowDefinitions.Add(rowDef2);
                TextBlock txt1 = new TextBlock();
                for (int q = 0; q < pictures.Length; q++)
                {
                    if (image.Name.Substring(2) == pictures[q].ID)
                    {
                        txt1.Text = pictures[q].Name;
                    }
                }
                txt1.TextWrapping = TextWrapping.Wrap;
                txt1.FontSize = 16;
                txt1.HorizontalAlignment = HorizontalAlignment.Center;
                txt1.FontWeight = FontWeights.Bold;
                Grid.SetRow(txt1, 0);
                Grid.SetRow(image, 1);
                grid.Children.Add(txt1);
                grid.Children.Add(image);
                imagesPanel.Children.Add(grid);

            }

            mainPictureNum = 0;

            /*BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(mainPicture.File);
            bi3.EndInit();*/

            UpdateMetaData();
        }

        private void ImageClicked(object sender, RoutedEventArgs e)
        {
            if (!(e.Source.GetType() == typeof(ImageWPF)))
                return;
            ImageWPF selectedImage = (ImageWPF) e.Source;
            BitmapImage bm = new BitmapImage();
            for (int i = 0; i < pictures.Length; i++)
            {
                if (pictures[i].ID == selectedImage.Name.Substring(2))
                {
                    for(int j = 0; j< filePaths.Length; j++)
                    {
                        if(selectedImage.Name.Substring(2) == Path.GetFileNameWithoutExtension(filePaths[j]))
                        {
                            bm.BeginInit();
                            bm.UriSource = new Uri(filePaths[j]);
                            bm.EndInit();
                            break;
                        }  
                    }
                    mainPictureNum = i;
                    break;
                }
            }
            UpdateMetaData();

            MainImage.Source = bm;
            string selectImagePath = MainImage.Source.ToString().Substring(8);
            Image sourceBitmap = Image.FromFile(selectImagePath);

           if(pictures[mainPictureNum].PercentOfBlue == 0.0 && pictures[mainPictureNum].PercentOfRed== 0.0 && pictures[mainPictureNum].PercentOfGreen == 0.0)
            {
                FindSaturation(selectedImage.Source.ToString().Substring(8), pictures[mainPictureNum]);
                Database.SetSaturation(pictures[mainPictureNum]);
            }
            sizeOfImage.Text = "Размер файла в пикселях: " + (sourceBitmap.Height * sourceBitmap.Width);
            resolutionOfImage.Text = "Разрешение файла: " + sourceBitmap.Width + "x" + sourceBitmap.Height;
        }

        private void FindSaturation(string selectImagePath, Picture picture)
        {
            Image sourceBitmap = Image.FromFile(selectImagePath);
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

            picture.PercentOfRed = procentRed;
            picture.PercentOfGreen = procentGreen;
            picture.PercentOfBlue = procentBlue;

            PercentOfRedLabel.Text = "Насыщенность красного: " + procentRed + "%";
            PercentOfGreenLabel.Text = "Насыщенность зеленого: " + procentGreen + "%";
            PercentOfBlueLabel.Text = "Насыщенность синего: " + procentBlue + "%";
           
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
           // currentPixel.Content = "Код текущего пикселя: " + ColorTranslator.ToHtml(bmp.GetPixel((int) Math.Round (p.X), (int) Math.Round (p.Y)));
        }

        private void OpenMap(object sender, RoutedEventArgs e)
        {
            string serialized = JsonConvert.SerializeObject(GetCollectionForMap());
            using (FileStream fstream = new FileStream(@"note.js", System.IO.FileMode.Create))
            {
                byte[] array = System.Text.Encoding.UTF8.GetBytes("var picturesFile = '" + serialized + "';");
                fstream.Write(array, 0, array.Length);
            }
            MapWindow map = new MapWindow();
            map.Show();
        }

        private void ChangePicture(object sender, RoutedEventArgs e)
        {
            ChangePictureWindow window = new ChangePictureWindow(pictures[mainPictureNum]);
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

        private void FindFace(object sender, RoutedEventArgs e)
        {
            string selectImagePath = AppDomain.CurrentDomain.BaseDirectory + "DataMini\\" + pictures[mainPictureNum].ID + ".jpg"; 
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
                    "Завершен поиск {0} лиц с помощью {1} за {2} мс",
                    faces.Count,
                    (iaImage.Kind == InputArray.Type.CudaGpuMat && CudaInvoke.HasCuda) ? "CUDA" :
                    (iaImage.IsUMat && CvInvoke.UseOpenCL) ? "OpenCL"
                    : "CPU",
                    detectionTime));
        }

        private void UpdateMetaData()
        {
            NameLabel.Text = "Название картины: " + pictures[mainPictureNum].Name;
            AuthorLabel.Text = "Автор картины: " + pictures[mainPictureNum].Author;
            YearOfCreationLabel.Text = "Дата создания: " + pictures[mainPictureNum].YearOfCreation;
            MaterialLabel.Text = "Материал: " + pictures[mainPictureNum].Material;
            RulesLabel.Text = "Правила использования: " + pictures[mainPictureNum].Rules;
            PercentOfRedLabel.Text = "Насыщенность красного: " + pictures[mainPictureNum].PercentOfRed + "%";
            PercentOfGreenLabel.Text = "Насыщенность зеленого: " + pictures[mainPictureNum].PercentOfGreen + "%";
            PercentOfBlueLabel.Text = "Насыщенность синего: " + pictures[mainPictureNum].PercentOfBlue + "%";
            PlaceOfStorageLabel.Text = "Место хранения: " + pictures[mainPictureNum].PlaceOfStorage;
            PlaceOfCreationLabel.Text = "Место создания: " + pictures[mainPictureNum].PlaceOfCreation;
        }

        private MapPicture[] GetCollectionForMap()
        {
            MapPicture[] mas = new MapPicture[pictures.Length];
            for (int i = 0; i < pictures.Length; i++)
            {
                mas[i].Author = pictures[i].Author.Replace('"', ' ');
                mas[i].Height = pictures[i].Height;
                mas[i].Width = pictures[i].Width;
                mas[i].ID = pictures[i].ID;
                mas[i].LatitudeCreation = pictures[i].LatitudeCreation;
                mas[i].LatitudeStorage = pictures[i].LatitudeStorage;
                mas[i].LongitudeCreation = pictures[i].LongitudeCreation;
                mas[i].LongitudeStorage = pictures[i].LongitudeStorage;
                mas[i].Material = pictures[i].Material;
                mas[i].Name = pictures[i].Name.Replace('"', ' ');
                mas[i].PercentOfBlue = pictures[i].PercentOfBlue;
                mas[i].PercentOfGreen = pictures[i].PercentOfGreen;
                mas[i].PercentOfRed = pictures[i].PercentOfRed;
                mas[i].YearMap = pictures[i].YearMap;
            }
            return mas;
        }

        private void OpenGraphic(object sender, RoutedEventArgs e)
        {
            Graphic windowGraph = new Graphic(pictures);
            windowGraph.Show();
        }
    }
}