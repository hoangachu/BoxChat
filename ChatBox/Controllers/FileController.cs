using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatBox.Controllers
{
    public class FileController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        public FileController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void SaveFile([FromForm(Name = "file")] IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "document");
            string filePath = Path.Combine(uploads, file.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }
        }
    }
}
