using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVC002.PL.Helpers
{
    public static class DocumentSettings
    { 
        public static string UploadFile(IFormFile file , string FolderName)
        {
            //string FolderPath = C:\Users\HP\source\repos\MVC002\MVC002\wwwroot\Files\Images
            //string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            //         string FileName = $"{Guid.NewGuid()}{file.FileName}"; 
            //         string FilePath = Path.Combine(FolderPath, FileName); //filepath = folderpath+ filename

            //         using var FileStream = new FileStream(FilePath,FileMode.Create);
            //         file.CopyTo(FileStream);

            //         return FileName;

            if (file == null || file.Length == 0)
                throw new ArgumentNullException(nameof(file), "File cannot be null or empty");

            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            // Ensure the folder exists
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            string FileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string FilePath = Path.Combine(FolderPath, FileName);

                    using var FileStream = new FileStream(FilePath,FileMode.Create);
                    file.CopyTo(FileStream);

            return FileName;
        }

        public static string DeleteFile(string filename , string foldername)
        {
            //Get File Path:
            //Check If Exist Or Not
            //If it exist,remove it 
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", foldername, filename);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            return filename;
        }
    }
}
