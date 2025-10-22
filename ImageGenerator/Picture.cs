namespace ImageGenerator
{
    internal class Picture
    {
        public required string File { get; set; }
        public required string Name { get; set; }
        public bool IsFavorite { get; set; }
        public string Comment { get; set; }
    }
}