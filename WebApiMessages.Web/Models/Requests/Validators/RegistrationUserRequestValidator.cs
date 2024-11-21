using FluentValidation;

namespace Messages.Web.Models.Requests.Validators
{
    public class RegistrationUserRequestValidator : AbstractValidator<RegistrationUserRequest>
    {
        public RegistrationUserRequestValidator()
        {
            RuleFor(x => x.Nick).Length(1, 20);
            RuleFor(x => x.Name).Length(2, 15);
            RuleFor(x => x.Password).Length(6, 10);
            RuleFor(x => x.RegistrationDate).NotEmpty();
        }
    }
}
