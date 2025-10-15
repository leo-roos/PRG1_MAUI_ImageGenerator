using System.Diagnostics;

namespace ImageGenerator
{
    public partial class MainPage : ContentPage
    {
        static private bool _isFavorite;

        Queue<Picture> ImageList = new Queue<Picture>();

        private Random random = new();

        public MainPage()
        {
            InitializeComponent();
            ImageList.Enqueue(new Picture { File = "image1", Name = "Man", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image2", Name = "Bird", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image3", Name = "Big cat", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image4", Name = "Autumn road", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image5", Name = "Flowergirl", IsFavorite = false });
        }

        private void ImageOnClicked(object? sender, EventArgs e)
        {
            ShowImageAndText();
        }

        private void ShowImageAndText()
        {
            Picture randomPicutre = ImageList.Dequeue();
            ImageList.Enqueue(randomPicutre);

            Debug.WriteLine(randomPicutre.File + ": " + randomPicutre.Name); // för testning i Output

            string showKey = GetImageFileEnding(randomPicutre.File); // detta då Windows, men inte till exempel Android, kräver filändelse

            ShowGallery.Source = showKey;

            ImageText.Text = randomPicutre.Name;
        }

        private string GetImageFileEnding(string imageKey)
        {
            #if WINDOWS
            return imageKey + ".jpg";
            #else
            return imageKey;
            #endif
        }

        private void OnFavoriteClicked(object sender, EventArgs e)
        {
            _isFavorite = !_isFavorite;

            if (_isFavorite)
            {
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87d",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Red
                };
            }
            else
            {
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87e",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Gray
                };
            }
        }
    }
}
