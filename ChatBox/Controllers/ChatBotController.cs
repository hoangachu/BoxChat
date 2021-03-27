using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ChatBox.Models.HistoryChat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ChatBox.Controllers
{
    public class ChatBotController : Controller
    {
        UserController _UserController;
        public ChatBotController(UserController UserController)
        {
            _UserController = UserController;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChatPreview(int SenderID, int ReceiverID)
        {
            ChatHistory ChatHistory = new ChatHistory();
            var User = _UserController.GetUserByUserID(SenderID);
            ChatHistory.UserID = SenderID;
            ChatHistory.ReceiverID = ReceiverID;
            ChatHistory.ListHistoryMessage = GetHistoryMessagesUser(SenderID, ReceiverID);
            ViewBag.imgURL = User.imgURL;


            return View(ChatHistory);

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
                            cmd.Parameters.Add("@SenderID", SqlDbType.VarChar).Value = SenderID;
                            cmd.Parameters.Add("@ReceiverID", SqlDbType.VarChar).Value = ReceiverID;
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                HistoryMessage historyMessage = new HistoryMessage();
                                historyMessage.UserID = (Int32)dr["UserID"];
                                historyMessage.MessageSendText = (string)dr["MessageSendText"];
                                historyMessage.SendTimer = (DateTime)dr["SendTimer"];
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
    }
}
