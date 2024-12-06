using FluentValidation;

namespace Messages.Web.Models.Requests.Validators
{
    public class AddContactRequestValidator : AbstractValidator<AddContactRequest>
    {
        public AddContactRequestValidator()
        {
            RuleFor(x => x.RecipiendId).NotEmpty(); 
            RuleFor(x => x.NameContact).Length(2, 20);
        }
    }
}
