using FluentValidation;


namespace Messages.Web.Models.Requests.Validators
{
    public class AddMessageRequestValidator : AbstractValidator<AddMessageRequest>
    {
        public AddMessageRequestValidator()
        {
            RuleFor(x => x.RecipiendId).NotEmpty();
            RuleFor(x => x.Message).MinimumLength(1);
            //RuleFor(x => x.SendDate).NotNull();
        }
    }
}
