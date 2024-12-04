
namespace Messages.Dal.Entityes
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsDelivered { get; set; }
        public Guid SenderId { get; set; }
        public UserEntity Sender { get; set; }
        public Guid RecipientId { get; set; }
        public UserEntity Recipient { get; set; }
    }
}
