using FluentValidation;
using Oss.Model.Request;

namespace Oss.Validators
{
    public class FlowValidator: AbstractValidator<Flow>
    {
        public FlowValidator()
        {
            RuleFor(x => x.FlowQos)
                .NotEmpty();
        }
    }
}
