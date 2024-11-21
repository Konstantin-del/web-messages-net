namespace Messages.Core.Models.Requests
{
    public class CreateContactRequest
    {
        public Guid IdRecipient { get; set; }
        public string Name { get; set; }
    }
}
