using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Models;
using FluentValidation;

namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Validators
{
    public class DeletePatientByIdHandlerValidator : AbstractValidator<DeletePatientByIdHandlerRequest>
    {
        public DeletePatientByIdHandlerValidator()
        {
            RuleFor(x => x.PatientId)
                .GreaterThan(0);
        }
    }
}
