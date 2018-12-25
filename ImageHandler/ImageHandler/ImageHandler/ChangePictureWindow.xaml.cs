using System.Windows;
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

            NameText.Text = picture.Name;
            AuthorText.Text = picture.Author;
            YearOfCreationText.Text = picture.YearOfCreation;
            MaterialText.Text = picture.Material;
            RulesText.Text = picture.Rules;
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
            try
            {
                picture.Name = NameText.Text;
                picture.Author = AuthorText.Text;
                picture.YearOfCreation = YearOfCreationText.Text;
                picture.Material = MaterialText.Text;
                picture.Rules = RulesText.Text;
                picture.Description = DescriptionText.Text;
                picture.PlaceOfCreation = PlaceOfCreationText.Text;
                picture.PlaceOfStorage = PlaceOfStorageText.Text;
                picture.LatitudeCreation = Convert.ToSingle(latitudePlaceOfCreationTextbox.Text.Replace(',', '.'));
                picture.LongitudeCreation = Convert.ToSingle(longitudePlaceOfCreationTextbox.Text.Replace(',', '.'));
                picture.LatitudeStorage = Convert.ToSingle(latitudePlaceOfStorageTextbox.Text.Replace(',', '.'));
                picture.LongitudeStorage = Convert.ToSingle(longitudePlaceOfStorageTextbox.Text.Replace(',', '.'));
                picture.YearMap = Convert.ToInt32(MapYearOfCreationTextbox.Text);
                picture.Height = Convert.ToSingle(HeightPictureTextbox.Text.Replace(',', '.'));
                picture.Width = Convert.ToSingle(WidthPictureTextbox.Text.Replace(',', '.'));
                Database.ChangePicture(picture);
                this.Close();
            }catch (FormatException)
            {
                MessageBox.Show("Неверный ввод");
            }

        }
    }
}
