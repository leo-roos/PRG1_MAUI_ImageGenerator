using System.Diagnostics;

namespace ImageGenerator
{
    public partial class MainPage : ContentPage
    {
        private Queue<Picture> ImageList = new Queue<Picture>();

        private Picture currentImage = new Picture { File = "", Name = "", IsFavorite = false };

        public MainPage()
        {
            InitializeComponent();
            ImageList.Enqueue(new Picture { File = "image1", Name = "Man", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image2", Name = "Bird", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image3", Name = "Big cat", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image4", Name = "Autumn road", IsFavorite = false });
            ImageList.Enqueue(new Picture { File = "image5", Name = "Flowergirl", IsFavorite = false });

            CommentEntry.IsEnabled = false;
        }

        private void ImageOnClicked(object? sender, EventArgs e)
        {
            ShowImageAndText();
        }

        private void ShowImageAndText()
        {
            currentImage = ImageList.Dequeue();
            ImageList.Enqueue(currentImage);

            Debug.WriteLine(currentImage.File + ": " + currentImage.Name); // för testning i Output

            string showKey = GetImageFileEnding(currentImage.File); // detta då Windows, men inte till exempel Android, kräver filändelse

            ShowGallery.Source = showKey;

            ImageText.Text = currentImage.Name;

            CommentEntry.IsEnabled = true;
            CommentEntry.Text = currentImage.Comment;
            SetFavoriteIcon(currentImage.IsFavorite);

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
            Debug.WriteLine($"Favorite clicked, {currentImage.File}: {currentImage.Name}, {currentImage.IsFavorite}"); // för testning i Output
            bool IsFavorite = !currentImage.IsFavorite;
            currentImage.IsFavorite = IsFavorite;

            SetFavoriteIcon(IsFavorite);
        }

        private void Comment_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentImage.Comment = CommentEntry.Text;
        }
    }
}
