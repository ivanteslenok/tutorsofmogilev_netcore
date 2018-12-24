using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorsOfMogilev_NetCore.Services
{
    // https://www.codeproject.com/Articles/1256591/%2FArticles%2F1256591%2FUpload-Image-to-NET-Core-2-1-API
    public class ImageService
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly string photosFolder;

        public ImageService(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            photosFolder = Path.Combine(_appEnvironment.WebRootPath, "uploads", "UsersPhotos");
        }

        private enum ImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
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

        public void DeleteOldPhoto(string oldPhoto)
        {
            var pathForDelete = photosFolder + $@"\{oldPhoto}";
            if (File.Exists(pathForDelete))
                File.Delete(pathForDelete);
        }

        public async Task<string> SavePhoto(IFormFile photo)
        {
            var photoName = Guid.NewGuid().ToString();
            var photoExtension = Path.GetExtension(photo.FileName);
            var photoFullName = $"{photoName}{photoExtension}";
            var path = photosFolder + $@"\{photoFullName}";

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
                fileStream.Flush();
            }

            return photoFullName;
        }

        private ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon

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
    }
}
