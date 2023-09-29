
using Extensions;
using FluentValidation;

using Oss.Requests;
using Oss.Model.Request;
using Oss.Services;

namespace Oss.Validators
{
    public class CreateSliceValidator: AbstractValidator<CreateSliceRequest>
    {
        private readonly DatabaseService db_;

        private int numSliceProfiles_ = 1;

        public CreateSliceValidator(DatabaseService db, IConfiguration configuration)
        {
            db_ = db;
            numSliceProfiles_ = int.Parse(configuration["Validation:SliceProfiles"] ?? "1");

            RuleFor(x => x.Intent.Name)
                .NotEmpty();

            RuleFor(x => x.Intent)
                .Must(UniqueNameAndId)
                .WithMessage("A slice with same id or same name is already in the system.");

            RuleFor(x => x.Intent.LocationConstraints)
                .ForEach(x => x.SetValidator(new ApplicationComponentLocationConstraintValidator(db)));

            RuleFor(x => x.Intent.SliceProfiles)
                .NotEmpty()
                .Must(ValidElementsNumber)
                .WithMessage($"Invalid number of Slice Profiles, must be {numSliceProfiles_}.")
                .ForEach(x => x.SetValidator(new SliceProfileValidator(db, configuration)));
        }

        private bool UniqueNameAndId(SliceIntent intent)
        {
            var instances = db_.GetVasInfos();

            return !instances.Any(i => i.VasConfiguration.Id == intent.Id || i.VasConfiguration.Name == intent.Name);
        }

        private bool ValidElementsNumber(IEnumerable<SliceProfile> items)
        {
            return numSliceProfiles_ == -1 || items?.Count() == numSliceProfiles_;
        }
    }
}
