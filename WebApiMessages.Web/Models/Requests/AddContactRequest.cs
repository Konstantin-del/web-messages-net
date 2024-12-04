namespace Messages.Web.Models.Requests
{
    public class AddContactRequest
    {
        public Guid RecipientId { get; set; }
        public string NameContact { get; set; }
    }
}
