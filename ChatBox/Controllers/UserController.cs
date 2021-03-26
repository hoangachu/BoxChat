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
        public IActionResult RegistUser(string UserName, string Password)
        {
            int UserID = 0;
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Startup.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Add_User", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Multis.Multis.Encrypt(Password);
                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        con.Open();
                         i = cmd.ExecuteNonQuery();
                        UserID = Convert.ToInt32(cmd.Parameters["@UserID"].Value);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(new { data = i, userID = UserID });
        }
    }
}
