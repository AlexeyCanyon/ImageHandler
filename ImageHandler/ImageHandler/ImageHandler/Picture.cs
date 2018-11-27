namespace ImageHandler
{
    public class Picture
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int YearOfCreation { get; set; }
        public string Rules { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public string Technology { get; set; }

        public float PercentOfRed { get; set; }
        public float PercentOfGreen { get; set; }
        public float PercentOfBlue { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float LongitudeCreation { get; set; }
        public float LatitudeCreation { get; set; }
        public float LongitudeStorage { get; set; }
        public float LatitudeStorage { get; set; }
    }
}