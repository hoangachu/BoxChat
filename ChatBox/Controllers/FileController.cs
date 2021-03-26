using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
        public IActionResult SaveFile([FromForm(Name = "file")] IFormFile file, [FromForm(Name = "userID")] int userID)
        {
            var i = 0;
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "document");
            var SystemFileName = file.FileName.Substring(0, file.FileName.LastIndexOf(".")) + "_" + userID.ToString() + "_" + DateTime.Today.Date.ToString("dd/MM/yyyy").Replace("/", "_") + System.IO.Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploads, SystemFileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Add_FileUpLoad", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = userID;
                    cmd.Parameters.Add("@FileURL", SqlDbType.VarChar).Value = filePath;
                    cmd.Parameters.Add("@OriginFileName", SqlDbType.VarChar).Value = file.FileName;
                    cmd.Parameters.Add("@SystemFileName", SqlDbType.VarChar).Value = SystemFileName;

                    con.Open();
                    i = cmd.ExecuteNonQuery();

                }
            }
            
                return Ok(new { data = i });
            
        }
    }
}
