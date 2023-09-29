
using Extensions;
using FluentValidation;
using Oss.Model.Request;

namespace Oss.Validators
{
    public class PduSessionValidator: AbstractValidator<PduSession>
    {
        private int numFlows_;
    
        public PduSessionValidator(IConfiguration configuration)
        {
            numFlows_ = int.Parse(configuration["Validation:NumFlows"] ?? "1");

            RuleFor(x => x.Flows)
                .NotEmpty()
                .Must(ValidElementsNumber)
                .WithMessage($"Invalid number of Flows, must be {numFlows_}.")
                .ForEach(x => x.SetValidator(new FlowValidator()));
        }

        private bool ValidElementsNumber(IEnumerable<Flow> items)
        {
            return numFlows_ == -1 || items?.Count() == numFlows_;
        }
    }
}
