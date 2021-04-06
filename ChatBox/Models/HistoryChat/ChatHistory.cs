using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.Models.HistoryChat
{
    public class ChatHistory
    {
        public int UserID { get; set; }
        public int ReceiverID { get; set; }
        //public List<HistoryMessage> ListHistoryMessage { get; set; }
        public int ChatMsgID { get; set; }
     
        public int MsgNumber { get; set; }
        public int ChkUser { get; set; }
        public string MessageSendText { get; set; }
        public string UserName { get; set; }
        public string ReceiverName { get; set; }
        public string ImagesUrlUser { get; set; }
        public string ImagesUrlReceiver { get; set; }
        public string SendTimer { get; set; }
        public List<Friend> LstFriend { get; set; }

    }
    public class Friend
    {
        public int UserID { get; set; }
        public int FriendId { get; set; }
        public string ImageURL { get; set; }
        public string FriendName { get; set; }
        public int Status { get; set; }
    }
}
