
using Extensions;
using FluentValidation;
using Oss.Model.Request;
using Oss.Services;

namespace Oss.Validators
{
    public class SliceProfileValidator: AbstractValidator<SliceProfile>
    {
        private static string[] validSliceTypes_ = new[] { "eMBB", "URLLC", "mMTC" };

        private readonly DatabaseService db_;
        private int numPduSessions_;

        public SliceProfileValidator(DatabaseService db, IConfiguration configuration)
        {
            db_ = db;
            numPduSessions_ = int.Parse(configuration["Validation:PduSessions"] ?? "1");

            RuleFor(x => x.SliceId)
                .NotEmpty();

            RuleFor(x => x.SliceType)
                .NotEmpty()
                .Must(ValidSliceType)
                .WithMessage("Invalid Slice type.");

            RuleFor(x => x.ProfileParams)
                .NotEmpty()
                .ChildRules(x => x
                    .RuleFor(p => p.PduSessions)
                    .NotEmpty()
                    .Must(ValidElementsNumber)
                    .WithMessage($"Invalid number of Pdu Sessions, must be {numPduSessions_}.")
                    .ForEach(x => x.SetValidator(new PduSessionValidator(configuration))));

            RuleFor(x => x.LocationConstraints)
                .NotEmpty()
                .Must(ValidAreaIds)
                .WithMessage("Invalid Location Constraints.")
                .ForEach(x => x.SetValidator(new SliceProfileLocationConstraintValidator(db)));
        }

        private bool ValidElementsNumber(IEnumerable<PduSession> items)
        {
            return numPduSessions_ == -1 || items?.Count() == numPduSessions_;
        }

        private bool ValidAreaIds(IEnumerable<LocationConstraint> locationConstraints)
        {
            if (locationConstraints is null || locationConstraints.IsEmpty())
            {
                return true;
            }

            var areas = db_.GetGeographicAreas();
            var requestedSliceAreaIds = locationConstraints?.Select(i => i.GeographicalAreaId);
            var slicesUrls = areas.Where(i => requestedSliceAreaIds.Contains(i.GeographicalAreaId)).Select(i => i.NfvclUrlTemplateFor5G).Distinct();

            return slicesUrls.Count() == 1;
        }

        private bool ValidSliceType(string sliceType)
        {
            return validSliceTypes_.Contains(sliceType);
        }
    }
}
