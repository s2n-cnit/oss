using FluentValidation;
using Oss.Model.Request;
using Oss.Services;

namespace Oss.Validators
{
    public class ApplicationComponentLocationConstraintValidator: AbstractValidator<ApplicationComponentLocationConstraint>
    {
        private readonly DatabaseService db_;

        public ApplicationComponentLocationConstraintValidator(DatabaseService db)
        {
            db_ = db;

            RuleFor(x => x.GeographicalAreaId)
                .NotEmpty()
                .Must(ValidArea)
                .WithMessage("The selected Geographical Area doesn't exist.");
        }

        public bool ValidArea(string id)
        {
            return db_.GeographicAreaExists(id);
        }
    }
}
