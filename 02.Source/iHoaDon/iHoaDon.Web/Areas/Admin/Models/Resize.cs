using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace iHoaDon.Web.Areas.Admin.Models
{
    public static class Resize
    {
        public static Image ResizeImage(Image img, int width, int height)
        {
            var b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.Bicubic; // Specify here
            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();

            return (Image)b;
        }
        public static string ThumbnailImage(HttpPostedFileBase file, int width, int height, string path)
        {
            try
            {
                Image img = ConvertPostedFileToImage(file);
                Image resizeImage = ResizeImage(img, width, height);
                resizeImage.Save(path, ImageFormat.Png);
                return path;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static Image ConvertPostedFileToImage(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                Image sourceimage = System.Drawing.Image.FromStream(file.InputStream);
                return sourceimage;
            }
            else
            {
                throw new Exception("Không tồn tại file!");
            }
        }
    }
}