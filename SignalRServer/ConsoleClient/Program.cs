using System;
using System.Threading.Tasks;
using ClientLibrary;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var chat = new Chat(ReadName, Read, WriteNewMessage);
            await chat.StartChat();
        }

        private static string ReadName()
        {
            Console.WriteLine("Set your nickname");
            return Console.ReadLine();
        }

        private static string Read()
        {
            var message = Console.ReadLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor);
            return message;
        }

        private static void WriteNewMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}