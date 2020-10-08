using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientLibrary
{
    public abstract class Chat
    {
        protected Chat()
        {
            _connection = ConnectionBuilder.ConfigureConnection();
            messages = new Subject<string>();
            messages.Subscribe(Write);
        }

        private readonly HubConnection _connection;
        private ISubject<string> messages;

        protected string Name { get; set; }

        protected abstract Task StartListening();

        protected async Task Send(string message)
        {
            await _connection.InvokeAsync("Send", message, Name);
        }

        protected abstract void Write(string message);

        protected abstract void ReadName();

        public async Task StartChat()
        {
            _connection.On<string, string>("sendMessage", (message, name) =>
            {
                messages.OnNext($"{Name}: {message}");
            });
            ReadName();
            await _connection.StartAsync();
            await Send($"{Name} is connected");
            await StartListening();
        }
    }
}