
using Messages.Dal.Entityes;

namespace Messages.Bll.ModelsBll
{
    public class ContactDto
    {
        public Guid OwnerId { get; set; }
        public Guid RecipiendId { get; set; }
        public string NameContact { get; set; }
    }
}
