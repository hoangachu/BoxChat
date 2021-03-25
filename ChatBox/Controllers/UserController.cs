using ChatBox.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public int RegistUser(IFormCollection base64img)
        {
            int i = 0;
            var UserName = base64img["UserName"];
            try
            {
                using (SqlConnection con = new SqlConnection(Startup.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Add_User", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        //cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                        //cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Multis.Multis.Encrypt(Password);

                        con.Open();
                        i = cmd.ExecuteNonQuery();
                    }
                }
                //if (!string.IsNullOrEmpty(base64img))
                //{
                //    string filePath = "MyImage.jpg";
                //    System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64img));
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }
    }
}
