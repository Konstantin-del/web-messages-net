namespace Messages.Core.Models.Requests
{
    public class MessageRequest
    {
        public Guid RecipientId { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
