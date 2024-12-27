using Easy.Application.Commands.EditarCandidato;
using FluentValidation;

namespace Easy.Application.Validators
{
    public class EditarCandidatoCommandValidator : AbstractValidator<EditarCandidatoCommand>
    {
        public EditarCandidatoCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("Id do candidato é obrigatório.");

            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(250).WithMessage("Nome não pode ter mais do que 150 caracteres!");

            RuleFor(t => t.DescricaoProfissional)
                .MaximumLength(500).WithMessage("Descrição Profissional não pode ter mais do que 1500 caracteres!");

            RuleForEach(t => t.Experiencias).SetValidator(new EditarExperienciaCommandValidator());

            RuleForEach(t => t.Formacoes).SetValidator(new EditarFormacaoCommandValidator());
        }
    }

    public class EditarExperienciaCommandValidator : AbstractValidator<EditarCandidatoCommand.EditarExperienciaCommand>
    {
        public EditarExperienciaCommandValidator()
        {
            RuleFor(e => e.Empresa)
                .NotEmpty().WithMessage("Empresa é obrigatória.")
                .MaximumLength(200).WithMessage("Empresa não pode ter mais do que 150 caracteres!");

            RuleFor(e => e.Cargos)
                .NotEmpty().WithMessage("Cargo é obrigatório.")
                .Must(cargos => cargos.Any()).WithMessage("Deve haver pelo menos um cargo.");

            RuleForEach(e => e.Cargos).SetValidator(new EditarCargoCommandValidator());
        }
    }

    public class EditarCargoCommandValidator : AbstractValidator<EditarCandidatoCommand.EditarCargoCommand>
    {
        public EditarCargoCommandValidator()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório.")
                .MaximumLength(150).WithMessage("Título não pode ter mais do que 150 caracteres!");

            RuleFor(c => c.DataInicial)
                .NotEmpty().WithMessage("Data Inicial é obrigatória.")
                .LessThanOrEqualTo(c => c.DataFinal == default ? DateTime.MaxValue : c.DataFinal)
                .WithMessage("Data Inicial deve ser menor ou igual à Data Final.");

            RuleFor(c => c.DataFinal)
                .NotEmpty().WithMessage("Data Final é obrigatória.");

            RuleFor(c => c.Descricao)
                .MaximumLength(500).WithMessage("Descrição não pode ter mais do que 500 caracteres!");
        }
    }

    public class EditarFormacaoCommandValidator : AbstractValidator<EditarCandidatoCommand.EditarFormacaoCommand>
    {
        public EditarFormacaoCommandValidator()
        {
            RuleFor(f => f.Instituicao)
                .NotEmpty().WithMessage("Instituição é obrigatória.")
                .MaximumLength(200).WithMessage("Instituição não pode ter mais do que 200 caracteres!");

            RuleFor(f => f.Curso)
                .NotEmpty().WithMessage("Curso é obrigatório.")
                .MaximumLength(150).WithMessage("Curso não pode ter mais do que 150 caracteres!");

            RuleFor(f => f.DataInicial)
                .NotEmpty().WithMessage("Data Inicial é obrigatória.");

            RuleFor(f => f.DataDeConclusao)
                .GreaterThanOrEqualTo(f => f.DataInicial).When(f => f.DataDeConclusao != default(DateTime))
                .WithMessage("Data de Conclusão deve ser maior ou igual à Data Inicial, caso informada.");
        }
    }
}
