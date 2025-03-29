using Messages.Dal.Entityes;

namespace Messages.Web.Models.Requests
{
    public class AddMessageRequest
    {
        public string Message { get; set; }
        public Guid RecipiendId { get; set; }
    }
}
