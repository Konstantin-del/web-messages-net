using FluentValidation;

namespace Messages.Web.Models.Requests.Validators;

public class DeleteMessageRequestValidator : AbstractValidator<DeleteMessageRequest>
{
    public DeleteMessageRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
    }
}
