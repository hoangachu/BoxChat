using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox.Models.HistoryChat
{
    public class ChatHistory
    {
        public int UserID { get; set; }
        public int ReceiveID { get; set; }
        public List<HistoryMessage> ListMessageOfUser { get; set; }
        public List<HistoryMessage> ListMessageOfReceiver { get; set; }
    }
    public class HistoryMessage
    {
        public int UserID { get; set; }
        public string MessageSendText { get; set; }
        public string  Images { get; set; }
        public DateTime  SendTimer { get; set; }
    }
}
