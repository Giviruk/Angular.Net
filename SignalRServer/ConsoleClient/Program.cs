using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var chat = new ConsoleChat();
            await chat.StartChat();
        }
    }
}