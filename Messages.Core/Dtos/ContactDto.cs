
namespace Messages.Core.Dtos
{
    public class ContactDto
    {
        public int id { get; set; } 
        public Guid IdRecipiend { get; set; }
        public string NameContact { get; set; }
        public UserDto Owner { get; set; }
    }
}
