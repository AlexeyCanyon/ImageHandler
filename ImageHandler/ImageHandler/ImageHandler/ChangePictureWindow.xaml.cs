using System.Windows;
using LiteDB;
using System;

namespace ImageHandler
{
    /// <summary>
    /// Логика взаимодействия для ChangePictureWindow.xaml
    /// </summary>
    public partial class ChangePictureWindow : Window
    {
        Picture picture;
        

        public ChangePictureWindow(Picture picture)
        {
            this.picture = picture;
            InitializeComponent();

            /*
            var db = new LiteDatabase(@"MyData.db");
            var PicturesCollection = db.GetCollection<Picture>("Pictures");
            
            if (PicturesCollection.FindById(picture.ID) != null)
            {
                Picture pic = PicturesCollection.FindById(picture.ID);
                latitudePlaceOfCreationTextbox.Text = pic.LatitudeCreation.ToString();
                longitudePlaceOfCreationTextbox.Text = pic.LongitudeCreation.ToString();
                latitudePlaceOfStorageTextbox.Text = pic.LatitudeStorage.ToString();
                longitudePlaceOfStorageTextbox.Text = pic.LongitudeStorage.ToString();
                MapYearOfCreationTextbox.Text = pic.YearOfCreation;
                HeightPictureTextbox.Text = pic.Height.ToString();
                WidthPictureTextbox.Text = pic.Width.ToString();
            }*/

            NameText.Text = picture.Name;
            AuthorText.Text = picture.Author;
            YearOfCreationText.Text = picture.YearOfCreation;
            MaterialText.Text = picture.Material;
            RulesText.Text = picture.Rules;
            TechnologyText.Text = picture.Technology;
            DescriptionText.Text = picture.Description;
            PlaceOfCreationText.Text = picture.PlaceOfCreation;
            PlaceOfStorageText.Text = picture.PlaceOfStorage;
            SourceSizePictureLabel.Content = "Исходные размеры: " + picture.Size;

            latitudePlaceOfCreationTextbox.Text = picture.LatitudeCreation.ToString();
            longitudePlaceOfCreationTextbox.Text = picture.LongitudeCreation.ToString();
            latitudePlaceOfStorageTextbox.Text = picture.LatitudeStorage.ToString();
            longitudePlaceOfStorageTextbox.Text = picture.LongitudeStorage.ToString();
            MapYearOfCreationTextbox.Text = picture.YearMap.ToString();
            HeightPictureTextbox.Text = picture.Height.ToString();
            WidthPictureTextbox.Text = picture.Width.ToString();

        }

        private void SavePicture(object sender, RoutedEventArgs e)
        {
            var db = new LiteDatabase(@"MyData.db");
            var PicturesCollection = db.GetCollection<Picture>("Pictures");

            if (latitudePlaceOfCreationTextbox.Text.Length>0 &&
               longitudePlaceOfCreationTextbox.Text.Length > 0 &&
               latitudePlaceOfStorageTextbox.Text.Length > 0 &&
               longitudePlaceOfStorageTextbox.Text.Length > 0 &&
               MapYearOfCreationTextbox.Text.Length > 0 &&
               HeightPictureTextbox.Text.Length > 0 &&
               WidthPictureTextbox.Text.Length > 0)
            {
                Picture pic = new Picture();
                pic.LatitudeCreation = Convert.ToSingle(latitudePlaceOfCreationTextbox.Text);
                pic.LongitudeCreation = Convert.ToSingle(longitudePlaceOfCreationTextbox.Text);
                pic.LatitudeStorage = Convert.ToSingle(latitudePlaceOfStorageTextbox.Text);
                pic.LongitudeStorage = Convert.ToSingle(longitudePlaceOfStorageTextbox.Text);
                pic.YearOfCreation = MapYearOfCreationTextbox.Text;
                pic.Height = Convert.ToInt32(HeightPictureTextbox.Text);
                pic.Width = Convert.ToInt32(WidthPictureTextbox.Text);
                pic.ID = picture.ID;
               

                picture.LatitudeCreation = pic.LatitudeCreation;
                picture.LongitudeCreation = pic.LongitudeCreation;
                picture.LatitudeStorage = pic.LatitudeStorage;
                picture.LongitudeStorage = pic.LongitudeStorage;
                picture.YearMap = Convert.ToInt32(pic.YearOfCreation);
                picture.Height = pic.Height;
                picture.Width = pic.Width;

                if(PicturesCollection.FindById(picture.ID) != null)
                {
                    PicturesCollection.Update(picture);
                } else
                {
                    PicturesCollection.Insert(pic);
                }
            }

            picture.Name = NameText.Text;
            picture.Author = AuthorText.Text;
            picture.YearOfCreation = YearOfCreationText.Text;
            picture.Material = MaterialText.Text;
            picture.Rules = RulesText.Text;
            picture.Technology = TechnologyText.Text;
            picture.Description = DescriptionText.Text;
            picture.PlaceOfCreation = PlaceOfCreationText.Text;
            picture.PlaceOfStorage = PlaceOfStorageText.Text;
            Database.ChangePicture(picture);
            this.Close();
        }
    }
}
