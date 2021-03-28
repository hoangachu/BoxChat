using ChatBox.Models;
using ChatBox.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatBox.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserController UserController = new UserController();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            int i = 0;
            User user = new User();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
               
                if (ModelState.IsValid)
                {
                    try
                    {
                        user = UserController.GetUserByUserName(Username);
                        var password = "";
                        if (!string.IsNullOrEmpty(user.Password))
                        {
                            password = Multis.Multis.Decrypt(user.Password).ToLower();
                        }

                        if (password.ToLower().Equals(Password.ToLower()))
                        {
                            i = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return Ok(Json(new { data = i, url = "/ChatBot/ChatPreview?UserID=" + user.UserID + "&&ReceiverID=0" }));
                //return Ok(Json(new { data = i, url = "/ChatBot/Emoji" }));
            }
        }

    }


}

