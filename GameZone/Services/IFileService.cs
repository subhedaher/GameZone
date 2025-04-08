namespace GameZone.Services
{
    public interface IFileService
    {
        Task<string> SaveImage(IFormFile formFile, string location);
        void DeleteImage(string name);
    }
}
