namespace Messages.Web.Models.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Nick { get; set; }

        public string? Token { get; set; }

        public List<GetContactsUserResponse>? Contacts { get; set; }
    }
}
