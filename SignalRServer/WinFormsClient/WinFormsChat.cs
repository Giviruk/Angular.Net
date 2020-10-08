using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using ClientLibrary;

namespace WinFormsClient
{
    public class WinFormsChat : Chat
    {
        private readonly ChatForm _form;
        
        public WinFormsChat(ChatForm form)
        {
            _form = form;
        }

        protected override Task StartListening()
        {
            _form.OnConnected();
            return Task.CompletedTask;
        }

        public async Task SendMessage(string message)
        {
            await Send(message);
        }

        protected override void Write(string message)
        {
            _form.ReceiveMessages(message);
        }

        protected override void ReadName()
        {
            while (string.IsNullOrEmpty(Name))
            {
                Thread.Sleep(200);
            }
        }

        public void SetName(string Name)
        {
            this.Name = Name;
        }
    }
}