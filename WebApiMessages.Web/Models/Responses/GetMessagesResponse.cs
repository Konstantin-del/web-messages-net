using Messages.Web.Models.Requests;

namespace Messages.Web.Models.Responses
{
    public class GetMessagesResponse
    {
        public List<MessageRequest> Messages { get; set; }
    }
}
