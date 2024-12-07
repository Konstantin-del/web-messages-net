using Messages.Web.Models.Requests;

namespace Messages.Web.Models.Responses
{
    public class GetMessagesResponse
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTimeOffset SendDate { get; set; }

        public Guid SenderId { get; set; }
    }
}
