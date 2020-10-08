using System;
using System.Threading.Tasks;
using ClientLibrary;

namespace ConsoleClient
{
    public class ConsoleChat : Chat
    {
        protected override async Task StartListening()
        {
            while (true)
            {
                var message = Console.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                await Send(message);
            }
        }
        
        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor);
        }

        protected override void Write(string message)
        {
            Console.WriteLine(message);
        }

        protected override void ReadName()
        {
            Console.WriteLine("Set your name");
            Name = Console.ReadLine();
        }
    }
}