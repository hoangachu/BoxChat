﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ChatBox.Models.HistoryChat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ChatBox.Controllers
{
    public class ChatBotController : Controller
    {
        UserController _UserController = new UserController();

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChatPreview(int UserID, int ReceiverID)
        {
            ChatHistory ChatHistory = new ChatHistory();
            var User = _UserController.GetUserByUserID(UserID);
            ChatHistory.UserID = UserID;
            ChatHistory.ReceiverID = ReceiverID;
            ChatHistory.ListHistoryMessage = GetHistoryMessagesUser(UserID, ReceiverID);
            ViewBag.imgURL = User.imgURL;
            ViewBag.UserID = User.UserID;
            ViewBag.UserName = User.UserName;
            ViewBag.tmptable = ChatHistory.ListHistoryMessage.FirstOrDefault().MessageSendText;
            return View(ChatHistory);

        }
        [HttpGet]
        public IActionResult Emoji()
        {

            return View();

        }
        public List<HistoryMessage> GetHistoryMessagesUser(int SenderID, int ReceiverID)
        {
            List<HistoryMessage> lstHistoryMessage = new List<HistoryMessage>();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetList_Message", con))
                {
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add("@SenderID", SqlDbType.VarChar).Value = 1020;
                            cmd.Parameters.Add("@ReceiverID", SqlDbType.VarChar).Value = 1020;
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                HistoryMessage historyMessage = new HistoryMessage();
                                historyMessage.UserID = (Int32)dr["UserID"];
                                historyMessage.MessageSendText = (string)dr["MessageSendText"];
                                var sendtimer = (DateTime)dr["SendTimer"];
                                historyMessage.SendTimer = sendtimer == null ? DateTime.Now.ToString("{0:MM/dd/yyyy}") : sendtimer.ToString("{0:MM/dd/yyyy}");
                                historyMessage.ImagesUrl = (string)dr["ImagesUrl"];
                                lstHistoryMessage.Add(historyMessage);
                            }
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }

                        con.Close();
                    }


                }
            }
            return lstHistoryMessage;
        }
    
        [HttpPost]
        public void CreatXMLFile(string userIDReceive, string message)

        {
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Update_ChatBotMessage", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = 1020;
                    cmd.Parameters.Add("@MessageText", SqlDbType.VarChar).Value = message;
                    cmd.Parameters.Add("@ReceiverID", SqlDbType.Int).Value = 1020;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
   
}
