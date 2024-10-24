using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OsuReplayAnalyser.Model.Images
{
    public static class ImageHandler
    {
        public static BitmapImage LoadImageFromFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException(filePath);
            }

            BitmapImage bitmap = new();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        public static BitmapSource LoadCroppedImageFromFile(string filePath)
        {
            BitmapSource bitmap = LoadImageFromFile(filePath);

            var width = bitmap.PixelWidth;
            var height = bitmap.PixelHeight;
            var stride = width * 4;
            var pixels = new byte[height * stride];
            bitmap.CopyPixels(pixels, stride, 0);

            int minX = width, maxX = 0, minY = height, maxY = 0;
            bool foundPixel = false;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = (y * stride) + (x * 4);
                    byte alpha = pixels[index + 3];
                    if (alpha > 0)
                    {
                        foundPixel = true;
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            if (!foundPixel) return bitmap;

            Int32Rect croppedRect = new Int32Rect(minX, minY, maxX - minX + 1, maxY - minY + 1);
            CroppedBitmap croppedBitmap = new CroppedBitmap(bitmap, croppedRect);

            return croppedBitmap;
        }
    }
}
