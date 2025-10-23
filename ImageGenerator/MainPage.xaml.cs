using System.Diagnostics;

namespace ImageGenerator
{
    public partial class MainPage : ContentPage
    {
        private int _currentIndex = -1;

        private List<Picture> ImageList = new()
        {
            new Picture { File = "image1", Name = "Man", IsFavorite = false },
            new Picture { File = "image2", Name = "Bird", IsFavorite = false },
            new Picture { File = "image3", Name = "Big cat", IsFavorite = false },
            new Picture { File = "image4", Name = "Autumn road", IsFavorite = false },
            new Picture { File = "image5", Name = "Flowergirl" , IsFavorite = false },
            new Picture { File = "img2350", Name = "Sick Red Cat" , IsFavorite = false },
            new Picture { File = "img2364", Name = "Broken down car" , IsFavorite = false },
            new Picture { File = "img2530", Name = "Cow" , IsFavorite = false },
            new Picture { File = "img2693", Name = "Penguins" , IsFavorite = false },
            new Picture { File = "img2801", Name = "Dog" , IsFavorite = false },
            new Picture { File = "img2869", Name = "Lilla Bommen in the Rain" , IsFavorite = false },
            new Picture { File = "img2888", Name = "Gokart" , IsFavorite = false },
            new Picture { File = "img2930", Name = "Red Cat Sleeping" , IsFavorite = false },
        };

        private Random random = new();

        public MainPage()
        {
            InitializeComponent();

            CommentEntry.IsEnabled = false;
            FavoriteButton.IsEnabled = false;
        }

        private void ImageOnClicked(object? sender, EventArgs e)
        {
            int randomIndex = random.Next(ImageList.Count);
            Picture randomPicture = ImageList.ElementAt(randomIndex);
            _currentIndex = randomIndex;

            ShowImageAndText();
        }

        /// <summary>
        /// Visa bilden och texten för den aktuella bilden i ImageList på skärmen.
        /// </summary>
        private void ShowImageAndText()
        {
            Picture currentImage = ImageList[_currentIndex];
            Debug.WriteLine(currentImage.File + ": " + currentImage.Name); // för testning i Output

            string showKey = GetImageFileEnding(currentImage.File); // detta då Windows, men inte till exempel Android, kräver filändelse

            ShowGallery.Source = showKey;

            ImageText.Text = currentImage.Name;

            CommentEntry.IsEnabled = true;
            CommentEntry.Text = currentImage.Comment;

            FavoriteButton.IsEnabled = true;
            SetFavoriteIcon(currentImage.IsFavorite);
        }

        /// <summary>
        /// Returnerar filens namn med korrekt filändelse beroende på plattform.
        /// </summary>
        /// <param name="imageKey">Fil namn</param>
        /// <returns>Fil namn med korrekt filändelse</returns>
        private string GetImageFileEnding(string imageKey)
        {
#if WINDOWS
            return imageKey + ".jpg";
#else
            return imageKey;
#endif
        }

        /// <summary>
        /// Ändrar favoritikonen beroende på om bilden är favorit eller inte.
        /// </summary>
        /// <param name="IsFavorite">Om den aktuella bilden är en favorit</param>
        private void SetFavoriteIcon(bool IsFavorite)
        {
            if (IsFavorite)
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
            Picture currentImage = ImageList[_currentIndex];
            Debug.WriteLine($"Favorite clicked, {currentImage.File}: {currentImage.Name}, {currentImage.IsFavorite}"); // för testning i Output
            bool IsFavorite = !currentImage.IsFavorite;
            currentImage.IsFavorite = IsFavorite;

            SetFavoriteIcon(IsFavorite);
        }

        private void Comment_TextChanged(object sender, TextChangedEventArgs e)
        {
            Picture currentImage = ImageList[_currentIndex];
            currentImage.Comment = CommentEntry.Text;
        }
    }
}
