using FluentValidation;


namespace Messages.Core.Models.Requests.Validators
{
    public class MessageRequestValidator : AbstractValidator<MessageRequest>
    {
        public MessageRequestValidator()
        {
            RuleFor(x => x.RecipientId).NotNull();
            RuleFor(x => x.Message).MinimumLength(1);
            RuleFor(x => x.CreateDate).NotNull();
        }
    }
}
