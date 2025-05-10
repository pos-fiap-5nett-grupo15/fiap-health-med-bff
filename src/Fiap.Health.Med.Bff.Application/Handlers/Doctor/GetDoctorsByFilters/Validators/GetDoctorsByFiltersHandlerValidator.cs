using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models;
using FluentValidation;

namespace Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Validators
{
    public class GetDoctorsByFiltersHandlerValidator : AbstractValidator<GetDoctorsByFiltersHandlerRequest>
    {
        public GetDoctorsByFiltersHandlerValidator()
        {
            RuleFor(x => x.DoctorDoncilNumber)
                .GreaterThan(0)
                .When(x => x.DoctorDoncilNumber.HasValue);
        }
    }
}
