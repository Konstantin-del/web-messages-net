using FluentValidation;

namespace Messages.Web.Models.Requests.Validators
{
    public class DeleteContactRequestValidator : AbstractValidator<DeleteContactRequest>
    {
        public DeleteContactRequestValidator()
        {
            RuleFor(x => x.IdRecipient).NotEmpty();
        }
    }
}
