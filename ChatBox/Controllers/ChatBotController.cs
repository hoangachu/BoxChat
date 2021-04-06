using System;
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
            ChatHistory.ImagesUrlUser = User.imgURL;
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
                                ChatHistory.UserID = (Int32)dr["UserID"]; 
                                ChatHistory.ReceiverID = (Int32)dr["ReceiverID"];
                                ChatHistory.MessageSendText = (string)dr["MessageSendText"];
                                //var sendtimer = (DateTime)dr["SendTimer"];
                                //ChatHistory.SendTimer = sendtimer == null ? DateTime.Now.ToString("{0:MM/dd/yyyy}") : sendtimer.ToString("{0:MM/dd/yyyy}");
                                //ChatHistory.ImagesUrlUser = (string)dr["ImagesUrlUser"];
                                ChatHistory.ImagesUrlReceiver = (string)dr["ImagesUrlReceiver"];
                                ChatHistory.UserName = (string)dr["UserName"];
                                ChatHistory.ReceiverName = (string)dr["ReceiverName"];
                                break;
                            }
                            dr.Close();
                            //ChatHistory.Add(historyMessage);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }

                        con.Close();
                    }


                }
            }
            ViewBag.imgURL = User.imgURL;
            ViewBag.UserID = User.UserID;
            ViewBag.UserName = User.UserName;
            ViewBag.tmptable = ChatHistory.MessageSendText;
            ChatHistory.LstFriend = GetListFriend(UserID);
            return View(ChatHistory);

        }
        [HttpGet]
        public List<Friend> GetListFriend(int UserId)
        {
            List<Friend> lstFriend = new List<Friend>();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetList_Friend", con))
                {
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserId;
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Friend Friend = new Friend();
                                Friend.UserID = (Int32)dr["UserID"];
                                Friend.FriendId = (Int32)dr["FriendID"];
                                Friend.Status = (Int32)dr["Status"];
                                //Friend.MessageSendText = (string)dr["MessageSendText"];
                                //var sendtimer = (DateTime)dr["SendTimer"];
                                //Friend.SendTimer = sendtimer == null ? DateTime.Now.ToString("{0:MM/dd/yyyy}") : sendtimer.ToString("{0:MM/dd/yyyy}");
                                Friend.ImageURL = (string)dr["ImageURL"];
                                Friend.FriendName = (string)dr["FriendName"];
                                //ChatHistory.ImagesUrlReceiver = (string)dr["ImagesUrlReceiver"];
                                //ChatHistory.UserName = (string)dr["UserName"];
                                //ChatHistory.ReceiverName = (string)dr["ReceiverName"];
                                lstFriend.Add(Friend);
                            }
                            dr.Close();
                            //ChatHistory.Add(historyMessage);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }

                        con.Close();
                    }


                }
            }

            return lstFriend;

        }
        //public List<HistoryMessage> GetHistoryMessagesUser(int SenderID, int ReceiverID)
        //{
        //    List<HistoryMessage> lstHistoryMessage = new List<HistoryMessage>();

        //    return lstHistoryMessage;
        //}

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
