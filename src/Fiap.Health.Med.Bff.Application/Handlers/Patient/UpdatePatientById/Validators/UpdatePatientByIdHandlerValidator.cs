using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Models;
using FluentValidation;

namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Validators
{
    public class UpdatePatientByIdHandlerValidator : AbstractValidator<UpdatePatientByIdHandlerRequest>
    {
        public UpdatePatientByIdHandlerValidator()
        {
            RuleFor(x => x.PatientId)
                .GreaterThan(0);
        }
    }
}
