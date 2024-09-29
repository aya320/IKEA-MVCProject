using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Common.Services.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private List<string> _AllowedExtensions = new() { ".png", ".jpg", ".jpeg" };
        private const int _AllowedMaxSize = 2097152;

        public string? Upload(IFormFile file, string FolderName)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!_AllowedExtensions.Contains(extension))
                return null;
            if(file.Length > _AllowedMaxSize) 
                return null;

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            if(!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            var FileName=$"{Guid.NewGuid()}{extension}";
            var FilePath =Path.Combine(FolderPath, FileName);
            var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);

            return FileName;
        }
        public bool Delete(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }
            else
                return false;
          
        }

      
    }
}
