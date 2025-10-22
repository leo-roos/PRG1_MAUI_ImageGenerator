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

        private Queue<Picture> ImageHistory = new Queue<Picture>();

        public MainPage()
        {
            InitializeComponent();

            CommentEntry.IsEnabled = false;
            FavoriteButton.IsEnabled = false;
            LastImageButton.IsEnabled = false;
        }

        private void LastImageOnClicked(object? sender, EventArgs e)
        {
            ImageHistory = new Queue<Picture>(ImageHistory.Reverse());
            Picture newPicture = ImageHistory.Dequeue();
            ImageHistory = new Queue<Picture>(ImageHistory.Reverse());

            _currentIndex = ImageList.IndexOf(newPicture);

            ShowImageAndText();
        }
        private void ImageOnClicked(object? sender, EventArgs e)
        {
            if (_currentIndex != -1)
            {
                ImageHistory.Enqueue(ImageList[_currentIndex]);
            }

            int randomIndex = random.Next(ImageList.Count);
            Picture randomPicture = ImageList.ElementAt(randomIndex);
            _currentIndex = randomIndex;

            ShowImageAndText();
        }

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

            if (ImageHistory.Count > 0)
            {
                LastImageButton.IsEnabled = true;
            } else
            {
                LastImageButton.IsEnabled = false;
            }
        }

        private string GetImageFileEnding(string imageKey)
        {
            #if WINDOWS
            return imageKey + ".jpg";
            #else
            return imageKey;
            #endif
        }

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
