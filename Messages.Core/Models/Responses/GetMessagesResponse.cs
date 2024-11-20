using Messages.Core.Models.Requests;

namespace Messages.Core.Models.Responses
{
    public class GetMessagesResponse
    {
        public List<MessageRequest> Messages { get; set; }
    }
}
