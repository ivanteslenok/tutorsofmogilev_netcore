using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace TutorsOfMogilev_NetCore.Services
{
    // https://www.codeproject.com/Articles/1256591/%2FArticles%2F1256591%2FUpload-Image-to-NET-Core-2-1-API
    public class ImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string photosFolder;
        private readonly int quality;
        private readonly int smallHeight;
        private readonly string originalPhotoPrefix;
        private readonly string smallPhotoPrefix;
        private enum ImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
        }

        public const string photoExtension = ".webp";

        public ImageService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;

            quality = configuration.GetSection("PhotoSettings").GetValue<int>("quality");
            smallHeight = configuration.GetSection("PhotoSettings").GetValue<int>("smallHeight");
            originalPhotoPrefix = configuration.GetSection("PhotoSettings").GetValue<string>("originalPrefix");
            smallPhotoPrefix = configuration.GetSection("PhotoSettings").GetValue<string>("smallPrefix");
            photosFolder = Path.Combine(webHostEnvironment.WebRootPath, @"uploads\UsersPhotos");
        }

        public bool IsImage(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return GetImageFormat(fileBytes) != ImageFormat.unknown;
        }

        public void DeleteOldPhoto(string oldPhotoName)
        {
            var photosMap = GetImagesPathes(oldPhotoName);

            foreach (var photoPath in photosMap)
            {
                if (File.Exists(photoPath.Value))
                {
                    File.Delete(photoPath.Value);
                }
            }
        }

        public string SavePhoto(Stream photo, string exsistingPhotoName = null)
        {
            var photoName = string.IsNullOrWhiteSpace(exsistingPhotoName)
                ? Guid.NewGuid().ToString()
                : exsistingPhotoName;
            var photosMap = GetImagesPathes(photoName);

            if (!Directory.Exists(photosFolder))
            {
                Directory.CreateDirectory(photosFolder);
            }

            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
            {
                var factory = imageFactory.Load(photo)
                            .Format(new WebPFormat());

                using (var webPFileStream = new FileStream(photosMap[originalPhotoPrefix], FileMode.Create))
                {
                    factory.Quality(quality).Save(webPFileStream);
                    webPFileStream.Flush();
                }

                using (var webPSmallFileStream = new FileStream(photosMap[smallPhotoPrefix], FileMode.Create))
                {
                    factory.Resize(new Size { Height = smallHeight }).Save(webPSmallFileStream);
                    webPSmallFileStream.Flush();
                }
            }

            return photoName;
        }

        private ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");        // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");       // GIF
            var png = new byte[] { 137, 80, 78, 71 };       // PNG
            var tiff = new byte[] { 73, 73, 42 };           // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };          // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };   // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };  // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }

        private Dictionary<string, string> GetImagesPathes(string name)
        {
            return new Dictionary<string, string>
            {
                { originalPhotoPrefix, Path.Combine(photosFolder, $"{name}{originalPhotoPrefix}{photoExtension}") },
                { smallPhotoPrefix, Path.Combine(photosFolder, $"{name}{smallPhotoPrefix}{photoExtension}") }
            };
        }
    }
}
