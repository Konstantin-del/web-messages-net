using Messages.Dal.Entityes;

namespace Messages.Web.Models.Responses
{
    public class AddContactRespons
    {
        public Guid RecipientId { get; set; }
        public string NameContact { get; set; }
    }
}
