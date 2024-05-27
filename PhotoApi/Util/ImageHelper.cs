using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PhotoApi.Util;


public static class ImageHelper
{
    public static string ApplyWatermark(string base64Image)
    {
        string base64Watermark = ConfigurationHelper.Configuration["Base64Watermark"];
        
        int width = int.Parse(ConfigurationHelper.Configuration["widthWatermark"]);

        // Convert base64 strings to byte arrays
        byte[] imageBytes = Convert.FromBase64String(base64Image.Split(',')[1]);
        byte[] watermarkBytes = Convert.FromBase64String(base64Watermark.Split(',')[1]);

        using (MemoryStream msImage = new MemoryStream(imageBytes))
        using (MemoryStream msWatermark = new MemoryStream(watermarkBytes))
        using (Image image = Image.FromStream(msImage))
        using (Image watermark = Image.FromStream(msWatermark))
        {
            Image resizedWatermark = ResizeImage(watermark, width);

            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Calcula a posição para centralizar a marca d'água
                int x = (image.Width - resizedWatermark.Width) / 2;
                int y = (image.Height - resizedWatermark.Height) / 2;

                graphics.DrawImage(resizedWatermark, new Rectangle(x, y, resizedWatermark.Width, resizedWatermark.Height));

                using (MemoryStream outputStream = new MemoryStream())
                {
                    image.Save(outputStream, ImageFormat.Jpeg);
                    byte[] outputBytes = outputStream.ToArray();
                    string base64Output = Convert.ToBase64String(outputBytes);
                    return "data:image/jpeg;base64," + base64Output;
                }
            }
        }
    }

    private static Image ResizeImage(Image image, int width)
    {
        int originalWidth = image.Width;
        int originalHeight = image.Height;
        float ratio = (float)width / originalWidth;
        int height = (int)(originalHeight * ratio);

        Bitmap resizedImage = new Bitmap(width, height);
        using (Graphics graphics = Graphics.FromImage(resizedImage))
        {
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.DrawImage(image, 0, 0, width, height);
        }
        return resizedImage;
    }
}
