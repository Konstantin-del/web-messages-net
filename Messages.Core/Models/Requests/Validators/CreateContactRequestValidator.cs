using FluentValidation;

namespace Messages.Core.Models.Requests.Validators
{
    public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull() ;
            RuleFor(x => x.Name).Length(2, 20);
        }
    }
}
