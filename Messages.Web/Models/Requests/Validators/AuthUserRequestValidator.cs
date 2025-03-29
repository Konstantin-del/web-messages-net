using FluentValidation;

namespace Messages.Web.Models.Requests.Validators
{
    public class AuthUserRequestValidator : AbstractValidator<AuthUserRequest>
    {
        public AuthUserRequestValidator()
        {
            RuleFor(x => x.Nick).Length(1, 20);
            RuleFor(x => x.Password).Length(2, 15); 
        }
    }
}
