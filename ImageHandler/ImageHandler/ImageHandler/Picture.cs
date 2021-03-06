namespace ImageHandler
{
    struct MapPicture
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Material { get; set; }
        public double PercentOfRed { get; set; }
        public double PercentOfGreen { get; set; }
        public double PercentOfBlue { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }

        public int YearMap { get; set; }
        public float LongitudeCreation { get; set; }
        public float LatitudeCreation { get; set; }
        public float LongitudeStorage { get; set; }
        public float LatitudeStorage { get; set; }
    }

    public class Picture
    {
        public string ID { get; set; }
        public string File { get; set; }

        public string Name { get; set; }
        public string Author { get; set; }
        public string YearOfCreation { get; set; }
        public string Rules { get; set; }
        public string Material { get; set; }
        public string PlaceOfCreation { get; set; }
        public string PlaceOfStorage { get; set; }
        public string Size { get; set; }

        public double PercentOfRed { get; set; }
        public double PercentOfGreen { get; set; }
        public double PercentOfBlue { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }

        public int YearMap { get; set; }
        public float LongitudeCreation { get; set; }
        public float LatitudeCreation { get; set; }
        public float LongitudeStorage { get; set; }
        public float LatitudeStorage { get; set; }
    }
}