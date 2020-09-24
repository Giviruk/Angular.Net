namespace SignalRServer.Models
{
    public class Message
    {
        public Message(string ownerName, string body)
        {
            OwnerName = ownerName;
            Body = body;
        }
        
        public string OwnerName { get; set; }
        public string Body { get; set; }
    }
}