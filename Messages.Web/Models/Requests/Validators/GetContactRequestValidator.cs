using FluentValidation;

namespace Messages.Web.Models.Requests.Validators;

public class GetContactRequestValidator : AbstractValidator<GetContactRequest>
{
    public GetContactRequestValidator()
    {
        RuleFor(x => x.Nick).NotNull();
    }
}
