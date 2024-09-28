using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Common.Services.Attachment
{
    internal interface IAttachmentService
    {
        string? Upload(IFormFile file ,string FolderName);
        bool Delete(string FilePath);


    }
}
