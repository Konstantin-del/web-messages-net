using FluentValidation;

namespace Messages.Web.Models.Requests.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Name).Length(1, 20);
        }
    }
}
