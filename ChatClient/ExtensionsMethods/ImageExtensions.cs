using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChatClient
{
    public static class ImageExtensions
    {
        public static BitmapSource ToBitmapSource(this Bitmap bitmap)
        {
            var sizeOptions = BitmapSizeOptions.FromEmptyOptions();
            var intPtr = bitmap.GetHbitmap();

            return Imaging.CreateBitmapSourceFromHBitmap(intPtr, IntPtr.Zero, Int32Rect.Empty, sizeOptions);
        }

        public static Bitmap ToBitmap(this BitmapSource source)
        {
            if (source == null)
                return null;
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int stride = width * ((source.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                source.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using var btm = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr);
                return new Bitmap(btm);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }

        public static byte[] GetBytes(this Bitmap bitmap)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
        }

        public static Bitmap GetBitmap(this byte[] bytes)
        {
            var converter = new ImageConverter();
            return (Bitmap)converter.ConvertTo(bytes, typeof(Bitmap));
        }
    }
}
