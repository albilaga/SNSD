namespace SNSD.Helper
{
    using System.Net;

    using Android.Graphics;

    public static class ImageHelper
    {
        /// <summary>
        ///     Method to download image from url
        /// </summary>
        /// <param name="url">link for image</param>
        /// <returns></returns>
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}