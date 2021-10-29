using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment.Models;
using Payment.Services;

namespace Payment.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IDataUploadService _dataUploadService;

        public TransactionController(IDataUploadService dataUploadService)
        {
            _dataUploadService = dataUploadService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(FileUploadModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataUploadService.UploadData(model.FileUpload))
                {
                    return StatusCode(200);
                }
                else
                {
                    ModelState.AddModelError("Model", "Data Invalid.");
                    return BadRequest();
                }
            }
            return View(model);
        }
    }
}