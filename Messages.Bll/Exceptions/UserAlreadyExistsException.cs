
namespace Messages.Bll.Exceptions;

public class UserAlreadyExistsException(string message) : Exception(message)
{ }
