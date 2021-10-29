using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Services
{
    public interface IDataUploadService
    {
        bool UploadData(IFormFile file);
    }
}
