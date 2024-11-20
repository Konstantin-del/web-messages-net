using FluentValidation;

namespace Messages.Core.Models.Requests.Validators
{
    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator() 
        {
            RuleFor(x => x.NameContact).Length(1, 20);
        }
    }
}
