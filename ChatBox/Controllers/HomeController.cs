﻿using ChatBox.Models;
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
        UserController userController;
        public HomeController(ILogger<HomeController> logger, UserController userController)
        {
            _logger = logger;
            userController = userController;
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

                try
                {
                     user = userController.GetUserByUserName(Username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return Ok(Json(new { data  = 1, url = "/ChatBot/ChatPreview?UserID="+ user.UserID + "&&ReceiverID=0" }));

        }
    }
}
