

namespace Messages.Bll.ModelsBll
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Nick { get; set; }

        public string? Token { get; set; }

        public List<ContactDto>? Contacts { get; set; }
    }
}
