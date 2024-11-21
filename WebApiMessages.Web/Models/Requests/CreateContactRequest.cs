namespace Messages.Web.Models.Requests
{
    public class CreateContactRequest
    {
        public Guid IdRecipient { get; set; }
        public string Name { get; set; }
    }
}
