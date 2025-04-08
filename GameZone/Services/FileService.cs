namespace GameZone.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task<string> SaveImage(IFormFile formFile, string location)
        {
            var image = $"{location}/{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";

            var folderPath = Path.Combine(_imagesPath, location);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var path = Path.Combine(_imagesPath, image);
            using var stream = File.Create(path);
            await formFile.CopyToAsync(stream);
            return image;
        }


        public void DeleteImage(string name)
        {
            var imagePath = Path.Combine(_imagesPath, name);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }
}
