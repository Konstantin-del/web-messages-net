
namespace Messages.Dal.Entityes
{
    public class ContactEntity
    {
        public Guid RecipiendId { get; set; }
        public string NameContact { get; set; }
        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; }
    }
}
