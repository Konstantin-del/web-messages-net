using FluentValidation;

namespace Messages.Web.Models.Requests.Validators
{
    public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(x => x.IdRecipient).NotNull() ;
            RuleFor(x => x.Name).Length(2, 20);
        }
    }
}
