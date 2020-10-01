using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientLibrary
{
    public class ConnectionBuilder
    {
        public static HubConnection ConfigureConnection()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/chat", (opts) =>
                {
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            // bypass SSL certificate
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => true;
                        return message;
                    };
                })
                .WithAutomaticReconnect()
                .Build();
            
            connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            return connection;
        }
    }
}