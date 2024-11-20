namespace Messages.Core.Models.Requests
{
    public class CreateContactRequest
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }
    }
}
