
using Messages.Dal.Entityes;

namespace Messages.Bll.ModelsBll;

public class MessageDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTimeOffset SendDate { get; set; }
    public bool IsDelivered { get; set; }
    public Guid SenderId { get; set; }
    public Guid RecipiendId { get; set; }
    
}
