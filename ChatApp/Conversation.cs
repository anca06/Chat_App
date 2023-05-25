using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Conversation
    {
        public struct SimpleMessage
        {
            public String timestamp;
            public String username;
            public String message;
            public bool sent;
        }
        private Contact _contact;
        private List<SimpleMessage> _fullMessage = new List<SimpleMessage>();

        public Conversation(Contact contact)
        {
            _contact = contact;
        }
        public List<SimpleMessage> Message
        {
            get {return _fullMessage; }
        }

        public void addMessage(String timestamp, String username, String message, bool sent)
        {
            _fullMessage.Add(new SimpleMessage() {timestamp = timestamp, username = username, message = message, sent = sent });
        }
        public Contact Contact { get { return _contact; } }

        public void toString()
        {
            Console.WriteLine("Conversatiile userului: " + this.Contact.Name);
            foreach (SimpleMessage message in _fullMessage)
            {
                Console.WriteLine(message.timestamp + "\t\t" + message.username + "\t\t" + message.message);
            }
        }
    }
}
