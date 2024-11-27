
namespace Messages.Dal.Entityes
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsDelivered { get; set; }
        public UserEntity Sender { get; set; }
        public UserEntity Recipient { get; set; }
    }
}
