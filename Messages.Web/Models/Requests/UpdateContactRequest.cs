namespace Messages.Web.Models.Requests
{
    public class UpdateContactRequest
    {
        public Guid RecipiendId { get; set; }
        public string NameContact { get; set; }
    }
}
