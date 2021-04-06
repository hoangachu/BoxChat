using ChatBox.Models.HistoryChat;
using ChatBox.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBox.Controllers
{
    public class UserController : Controller
    {
        ChatBotController chatBotController = new ChatBotController();
        public IActionResult Index()
        {
            return View();
        }

        public User GetUserByUserID(int UserID)
        {
            User User = new User();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserByUserID", con))
                {
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;

                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                User.UserName = (string)dr["UserName"];
                                User.Password = (string)dr["Password"];
                                User.UserID = (int)dr["UserID"];
                                User.imgURL = (string)dr["ImagesUrl"];
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
            return User;
        }
        public User GetUserByUserName(string UserName)
        {
            User User = new User();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserByUserName", con))
                {
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;

                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                User.UserName = (string)dr["UserName"];
                                User.Password = (string)dr["Password"];
                                User.UserID = (int)dr["UserID"];
                                User.imgURL = (string)dr["FileURL"];
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
            return User;
        }
        [HttpPost]
        public IActionResult RegistUser([FromForm(Name = "file")] IFormFile file, [FromForm(Name = "UserName")] string UserName, [FromForm(Name = "Password")] string Password)
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
                        FileController.SaveFile(file, UserID);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(new { data = i, userID = UserID, url = "Home" });
        }
        [HttpPost]
        public void CheckStatus(int userID)
        {
            List<Friend> lstFriend = chatBotController.GetListFriend(userID);
            Update(lstFriend,Multis.TypeUpdateStatus.UPDATE_MIDDLECALL.GetHashCode(),userID);
            Thread.Sleep(2500);
            Update(lstFriend, Multis.TypeUpdateStatus.UPDATE_NOTWORKING.GetHashCode(),userID);
        }

        [HttpPost]
        public void Update(List<Friend> lstFriend,int typeUpdate,int userID)
        {
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Update_FriendStatus", con))
                {
                    var table = new DataTable();
                    table.Columns.Add("Item", typeof(string));
                    lstFriend.ForEach(x => table.Rows.Add(x));
                    var pList = new SqlParameter("@list", SqlDbType.Structured);
                    pList.TypeName = "dbo.listID";
                    pList.Value = table;
                    cmd.Parameters.Add(pList);
                    cmd.Parameters.AddWithValue(@"typeUpdate", typeUpdate);
                    cmd.Parameters.AddWithValue(@"userID", userID);
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }

        }
    }
}
