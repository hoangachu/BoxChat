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
        public List<HistoryMessage> ListHistoryMessage { get; set; }
      
    }
    public class HistoryMessage
    {
        public int ChatMsgID { get; set; }
        public int UserID { get; set; }
        public int MsgNumber { get; set; }
        public int ChkUser { get; set; }
        public string MessageSendText { get; set; }
        public string  ImagesUrl { get; set; }
        public string  SendTimer { get; set; }
    }
}
