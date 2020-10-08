using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientLibrary;

namespace WinFormsClient
{
    public partial class ChatForm : Form
    {
        private WinFormsChat WinFormsChat { get; set; }

        public ChatForm()
        {
            InitializeComponent();

            loginButton.Enabled = true;
            sendButton.Enabled = false;
            chatTextBox.ReadOnly = true;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            WinFormsChat.SetName(userNameTextBox.Text);
            userNameTextBox.ReadOnly = true;
            loginButton.Enabled = false;
        }

        public void ReceiveMessages(string message)
        {
            Invoke(new MethodInvoker(() => { chatTextBox.Text = chatTextBox.Text + "\r\n" + message; }));
        }

        private async void sendButton_Click(object sender, EventArgs e)
        {
            var message = messageTextBox.Text;
            await WinFormsChat.SendMessage(message);
            messageTextBox.Clear();
        }

        private async void ChatForm_Load(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                WinFormsChat = new WinFormsChat(this);
                await WinFormsChat.StartChat();
            });
        }

        public void OnConnected()
        {
            sendButton.Enabled = true;
        }
    }
}