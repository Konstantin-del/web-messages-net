namespace Messages.Web.Models.Requests
{
    public class AddContactRequest
    {
        public Guid RecipiendId { get; set; }
        public string NameContact { get; set; }
    }
}
