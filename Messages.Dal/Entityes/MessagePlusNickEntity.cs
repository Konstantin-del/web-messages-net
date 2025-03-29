
namespace Messages.Dal.Entityes;

public class MessagePlusNickEntity
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTimeOffset SendDate { get; set; }
    public Guid RecipiendId { get; set; }
    public Guid SenderId { get; set; }
    public string Nick { get; set; }
    public bool IsDelivered { get; set; }
}
