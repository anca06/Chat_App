using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ChatApp
{
    class Chat
    {
        private List<Conversation> _conversations = new List<Conversation>();
        private List<Contact> _contacts = new List<Contact>();

        public List<Conversation> Conversations
        {
            get { return _conversations; }
        }
        public List<Contact> Contacts
        {
            get { return _contacts; }
        }
        public Contact Contact(String name)
        {
            foreach(Contact contact in _contacts)
            {
                if (contact.Name.Equals(name))
                {
                    return contact;
                }
            }
            return null;
        }

        public Conversation Conversation(String name)
        {
            foreach (Conversation conversation in _conversations)
            {
                if (conversation.Contact.Name.Equals(name))
                {
                    return conversation;
                }
            }
            return null;
        }
        public void AddContact(String username)
        {
            _contacts.Add(new Contact(username));
        }
        public void AddContact(String username, Bitmap image)
        {
            _contacts.Add(new Contact(username, image));
        }
        public void RemoveContact(String username)
        {
            foreach (Contact contact in _contacts)
            {
                if (contact.Name.Equals(username))
                {
                    _contacts.Remove(contact);
                    break;
                }
            }
        }
        public void AddConversation(String timestamp, String username, String message, bool sent, Contact contact)
        {
            try
            {
                _conversations.Find(conversation => conversation.Contact.Name == contact.Name).addMessage(timestamp, username, message, sent);
                _conversations.Find(conversation => conversation.Contact.Name == username).addMessage(timestamp, contact.Name, message, sent);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        public List<Conversation.SimpleMessage> getConversationWithUser(Contact user, String username)
        {
            try
            {
                List<Conversation.SimpleMessage> user_conversations = new List<Conversation.SimpleMessage>();
                foreach (Conversation.SimpleMessage conversation in Conversation(user.Name).Message)
                {
                    if (conversation.username.Equals(username))
                        user_conversations.Add(conversation);
                }
                return user_conversations;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); return null;
            }
        }

        public void RemoveConversation()
        {

        }

        public void LoadContacts(Contact user)
        {
            String content = File.ReadAllText("./Contacts/Usernames.txt");
            String[] usernames = content.Split('/');

            String[] paths = Directory.GetFiles("./Contacts/ProfileImages");


            try
            {
                foreach (String username in usernames)
                {
                    if (username != user.Name) { }
                    bool check = false;
                    foreach (String path in paths)
                    {
                        Console.WriteLine(path + "   \\" + username + ".jpeg");
                        if (path.EndsWith("\\" + username + ".jpeg"))
                        {
                            _contacts.Add(new Contact(username, new Bitmap(path)));
                            check = true;
                            break;
                        }
                    }
                    if (check == false)
                    {
                        _contacts.Add(new Contact(username, new Bitmap("./Contacts/ProfileImages/noimage.jpeg")));
                    }
                }


            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void loadConversations(Contact user)
        {
            try {
                String[] usernames = File.ReadAllText("./Contacts/Usernames.txt").Split('/');
                String[] messages = File.ReadAllLines("./Contacts/Messages.txt");
                foreach (String username in usernames)
                {
                    Conversation currentConversation = new Conversation(this.Contact(username));

                    foreach (String message in messages)
                    {
                        String[] info = message.Split(';');
                        Console.WriteLine(info[0] + '-' + username);
                        if (info[0] == username) {
                            currentConversation.addMessage(info[2], info[1], info[3], true);
                        }
                        else if (info[1] == username) {
                            currentConversation.addMessage(info[2], info[0], info[3], false);
                        }
                    }
                    _conversations.Add(currentConversation);
                }
               
                foreach (Conversation conversation in _conversations) {
                    conversation.toString();
                }

            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }
    }
}
