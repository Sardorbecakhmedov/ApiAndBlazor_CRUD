using Microsoft.AspNetCore.Http;

namespace ApiProjectData.Services;

public class FileHelper
{
    private const string RootFolderPath = "wwwroot";
    private const string ImagesFolderPath = "ProductImages";
    private const string DefaultImagePath = "noPhoto.jpeg";

    private static void CreateFolder(string folderPath)
    {
        if (!Directory.Exists(Path.Combine(RootFolderPath, folderPath)))
        {
            Directory.CreateDirectory(Path.Combine(RootFolderPath, folderPath));
        }
    }

    public static async Task<string> SaveProductFile(IFormFile? file)
    {
        return await SaveFile(file, ImagesFolderPath);
    }

    private static async Task<string> SaveFile(IFormFile? file, string folderName)
    {
        if (file == null)
        {
            return $"/{folderName}/{DefaultImagePath}";
        }

        CreateFolder(folderName);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);


        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            await File.WriteAllBytesAsync(Path.Combine(RootFolderPath, folderName, fileName), ms.ToArray());
        }

        return $"/{folderName}/{fileName}";
    }

    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}