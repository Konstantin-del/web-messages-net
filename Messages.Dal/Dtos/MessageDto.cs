
namespace Messages.Dal.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsDelivered { get; set; }
        public UserDto Sender { get; set; }
        public UserDto Recipient { get; set; }
    }
}
