
namespace Messages.Dal.Entityes
{
    public class ContactEntity
    {
        public int id { get; set; } 
        public Guid IdRecipiend { get; set; }
        public string NameContact { get; set; }
        public UserEntity Owner { get; set; }
    }
}
