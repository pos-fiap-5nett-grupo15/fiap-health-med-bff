using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Models;
using FluentValidation;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Validators
{
    public class CreateScheduleHandlerValidator : AbstractValidator<CreateScheduleHandlerRequest>
    {
        public CreateScheduleHandlerValidator()
        {
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
