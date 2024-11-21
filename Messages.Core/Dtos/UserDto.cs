
namespace Messages.Core.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<ContactDto> Contacts { get; set; }
    }
}
