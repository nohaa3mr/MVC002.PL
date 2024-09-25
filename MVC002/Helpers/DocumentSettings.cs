using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVC002.PL.Helpers
{
    public static class DocumentSettings
    { 
        public static string UploadFile(IFormFile file , string FolderName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
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
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", foldername, filename);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            return filename;
        }
    }
}
