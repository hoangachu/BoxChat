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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChatPreview(int SenderID, int ReceiverID)
        {
            ChatHistory ChatHistory = new ChatHistory();
            ChatHistory.UserID = SenderID;
            ChatHistory.ReceiverID = ReceiverID;
            //ChatHistory.ListHistoryMessage = GetHistoryMessagesUser(SenderID, ReceiverID);
            ChatHistory.ListHistoryMessage = new List<HistoryMessage>();
            ChatHistory.ListHistoryMessage.Add(new HistoryMessage
            {
                ChkUser = 1,
                MessageSendText = "anh yêu me",
                SendTimer = "2019/09/24 14:21",
                UserID = 12,
                MsgNumber = 1,
                Images="/images/messenger.jpg"

            });
            return View(ChatHistory);

        }
        public List<HistoryMessage> GetHistoryMessagesUser(int SenderID, int ReceiverID)
        {
            List<HistoryMessage> lstHistoryMessage = new List<HistoryMessage>();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Add_User", con))
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
                                historyMessage.SendTimer = (string)dr["SendTimer"];
                                historyMessage.Images = (string)dr["Images"];
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
