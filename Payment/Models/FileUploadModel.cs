using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class FileUploadModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [AllowedExtensions(new string[] { ".csv", ".xml" })]
        public IFormFile FileUpload { get; set; }
    }
}
