using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Models;
using FluentValidation;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Validators
{
    public class UpdateScheduleHandlerValidator : AbstractValidator<UpdateScheduleHandlerRequest>
    {
        public UpdateScheduleHandlerValidator()
        {
            RuleFor(x => x.ScheduleId)
                .GreaterThan(0)
                .WithMessage("O Id do agendamento deve ser informado");

            RuleFor(x => x.DoctorId)
                .GreaterThan(0)
                .WithMessage("O Id do médico deve ser informado");

            RuleFor(x => x.ScheduleTime)
                .NotEmpty()
                .WithMessage("A data e hora do agendamento devem ser informadas")
                .Must(e => e > DateTime.Now)
                .WithMessage("A data do agendamento deve ser maior que a data atual");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .LessThan(10001)
                .WithMessage("O preço da consulta deve estar entre 1 e 10000");
        }
    }
}
