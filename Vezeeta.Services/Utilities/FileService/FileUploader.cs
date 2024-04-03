using Microsoft.AspNetCore.Http;

namespace Vezeeta.Services.Utilities.FileService;

internal class FileUploader
{
    public static string Upload(IFormFile photo, string root)
    {
        string uploadsFolder = Path.Combine(root, "images");
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            photo.CopyTo(fileStream);
        }
        return uniqueFileName;
    }
}
