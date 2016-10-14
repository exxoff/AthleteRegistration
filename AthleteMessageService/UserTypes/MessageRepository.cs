using AthleteMessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteMessageService.UserTypes
{
    public sealed class MessageRepository : IMessageRepository
    {

        static readonly MessageRepository _instance = new MessageRepository();
        public static MessageRepository Instance
        {
            get
            {
                return _instance;
            }
        }
        private static ObservableCollection<string> messages;

        public static ObservableCollection<string> Messages
        {
            get
            {
                if(messages == null)
                {
                    messages = new ObservableCollection<string>();
                }
                return messages;
            }
            set { messages = value; }
        }
        private MessageRepository()
        {
            
        }

        public void ReceiveMessage(string Message)
        {
            
            Messages.Add(Message);
        }
    }
}
