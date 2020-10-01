using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientLibrary
{
    public class Chat
    {
        private HubConnection connection;
        private string name;
        private Func<string> readName;
        private Func<string> read;
        private ISubject<string> messages;

        public Chat(Func<string> readName, Func<string> read, Action<string> writeNewMessage)
        {
            this.readName = readName;
            this.read = read;
            messages = new Subject<string>();
            messages.Subscribe(writeNewMessage);
            connection = ConnectionBuilder.ConfigureConnection();
        }

        private async Task StartListening()
        {
            while (true)
            {
                var message = read();
                await Send(message);
            }
        }

        protected async Task Send(string message)
        {
            await connection.InvokeAsync("Send", message, name);
        }
        
        public async Task StartChat()
        {
            connection.On<string, string>("sendMessage", (message, name) =>
            {
                messages.OnNext($"{name}: {message}");
            });
            name = readName();
            await connection.StartAsync();
            await Send($"{name} is connected");
            await StartListening();
        }
    }
}