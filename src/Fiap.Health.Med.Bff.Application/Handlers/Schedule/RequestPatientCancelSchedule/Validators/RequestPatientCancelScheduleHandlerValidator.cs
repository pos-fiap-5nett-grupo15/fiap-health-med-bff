using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Models;
using FluentValidation;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Validators
{
    public class RequestPatientCancelScheduleHandlerValidator : AbstractValidator<RequestPatientCancelScheduleHandlerRequest>
    {
        public RequestPatientCancelScheduleHandlerValidator()
        {
            RuleFor(x => x.ScheduleId)
                .NotEmpty()
                .WithMessage("O id da consulta não pode ser vazio.");
            RuleFor(x => x.PatientId)
                .NotEmpty()
                .WithMessage("O id do paciente não pode ser vazio.");
            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("A razão não pode ser vazia.");
        }
    }
}
