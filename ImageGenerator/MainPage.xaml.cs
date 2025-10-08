using System.Diagnostics;

namespace ImageGenerator
{
    public partial class MainPage : ContentPage
    {
        private int _currentIndex = -1;

        private List<Picture> Pictures = new()
        {
            new Picture { File = "image1", Name = "Man", IsFavorite = false },
            new Picture { File = "image2", Name = "Bird", IsFavorite = false },
            new Picture { File = "image3", Name = "Big cat", IsFavorite = false },
            new Picture { File = "image4", Name = "Autumn road", IsFavorite = false },
            new Picture { File = "image5", Name = "Flowergirl" , IsFavorite = false }
        };

        private Random random = new();

        public MainPage()
        {
            InitializeComponent();
        }

        private void ImageOnClicked(object? sender, EventArgs e)
        {
            ShowImageAndText();
        }

        private void ShowImageAndText()
        {
            int randomIndex = random.Next(Pictures.Count);
            Picture randomPicture = Pictures.ElementAt(randomIndex);
            _currentIndex = randomIndex;

            Debug.WriteLine(randomPicture.File + ": " + randomPicture.Name); // för testning i Output

            string showKey = GetImageFileEnding(randomPicture.File); // detta då enbart Windows kräver filändelse

            ShowGallery.Source = showKey;

            ImageText.Text = randomPicture.Name;

            SetFavoriteButtonState();
        }

        private string GetImageFileEnding(string imageKey)
        {
            #if WINDOWS
            return imageKey + ".jpg";
            #else
            return imageKey;
            #endif
        }

        private void SetFavoriteButtonState()
        {
            bool isFavorite = Pictures[_currentIndex].IsFavorite;
            if (isFavorite)
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


        private void OnFavoriteClicked(object sender, EventArgs e)
        {
            if (_currentIndex == -1) return;

            bool isFavorite = Pictures[_currentIndex].IsFavorite;
            Pictures[_currentIndex].IsFavorite = !isFavorite;

            SetFavoriteButtonState();
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string name = null)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
