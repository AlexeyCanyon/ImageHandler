using System.Windows;

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
            TechnologyText.Text = picture.Technology;
            DescriptionText.Text = picture.Description;
            PlaceOfCreationText.Text = picture.PlaceOfCreation;
            PlaceOfStorageText.Text = picture.PlaceOfStorage;
        }

        private void SavePicture(object sender, RoutedEventArgs e)
        {
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
