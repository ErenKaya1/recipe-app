using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RecipeApp.Service.Contracts;

namespace RecipeApp.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly string _webRootPath;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _webRootPath = hostEnvironment.WebRootPath;
        }

        public async Task SaveRecipeImage(IFormFile imageFile, string suffix)
        {
            var imagesPath = Path.Combine(_webRootPath, "image", "recipe");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var imagePath = Path.Combine(imagesPath, Path.GetFileNameWithoutExtension(imageFile.FileName) + "-" + suffix + Path.GetExtension(imageFile.FileName));

            using (var stream = new FileStream(imagePath, FileMode.Create))
                await imageFile.CopyToAsync(stream);
        }

        public async Task SaveRecipeImage(IFormFile imageFile, string suffix, string oldImageName)
        {
            var imagesPath = Path.Combine(_webRootPath, "image", "recipe");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            if (!string.IsNullOrEmpty(oldImageName) && File.Exists(Path.Combine(imagesPath, oldImageName)))
                File.Delete(Path.Combine(imagesPath, oldImageName));

            var imagePath = Path.Combine(imagesPath, Path.GetFileNameWithoutExtension(imageFile.FileName) + "-" + suffix + Path.GetExtension(imageFile.FileName));

            using (var stream = new FileStream(imagePath, FileMode.Create))
                await imageFile.CopyToAsync(stream);
        }

        public void DeleteRecipeImage(string ImageName)
        {
            if (File.Exists(Path.Combine(_webRootPath, "image", "recipe", ImageName)))
                File.Delete(Path.Combine(_webRootPath, "image", "recipe", ImageName));
        }

        public async Task SaveCategoryImage(IFormFile imageFile, string suffix)
        {
            var imagesPath = Path.Combine(_webRootPath, "image", "category");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var imagePath = Path.Combine(imagesPath, Path.GetFileNameWithoutExtension(imageFile.FileName) + "-" + suffix + Path.GetExtension(imageFile.FileName));

            using (var stream = new FileStream(imagePath, FileMode.Create))
                await imageFile.CopyToAsync(stream);
        }

        public async Task SaveCategoryImage(IFormFile imageFile, string suffix, string oldImageName)
        {
            var imagesPath = Path.Combine(_webRootPath, "image", "category");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var oldPath = Path.Combine(imagesPath, oldImageName);
            System.Console.WriteLine(oldPath);
            if (!string.IsNullOrEmpty(oldImageName) && File.Exists(oldPath))
                File.Delete(Path.Combine(imagesPath, oldImageName));

            var imagePath = Path.Combine(imagesPath, Path.GetFileNameWithoutExtension(imageFile.FileName) + "-" + suffix + Path.GetExtension(imageFile.FileName));

            using (var stream = new FileStream(imagePath, FileMode.Create))
                await imageFile.CopyToAsync(stream);
        }

        public void DeleteCategoryImage(string ImageName)
        {
            if (File.Exists(Path.Combine(_webRootPath, "image", "category", ImageName)))
                File.Delete(Path.Combine(_webRootPath, "image", "category", ImageName));
        }
    }
}