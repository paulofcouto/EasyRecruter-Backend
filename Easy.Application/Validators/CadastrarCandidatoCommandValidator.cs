using Easy.Application.Commands.CadastrarCandidato;
using FluentValidation;

namespace Easy.Application.Validators
{
    public class CadastrarCandidatoCommandValidator : AbstractValidator<CadastrarCandidatoCommand>
    {
        public CadastrarCandidatoCommandValidator()
        {
            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(150).WithMessage("Nome não pode ter mais do que 150 caracteres!");

            RuleFor(t => t.DescricaoProfissional)
                .MaximumLength(1500).WithMessage("Descrição Profissional não pode ter mais do que 1500 caracteres!");

            RuleForEach(t => t.Experiencias)
                .SetValidator(new CadastrarExperienciaCommandValidator())
                .When(t => t.Experiencias != null);

            RuleForEach(t => t.Formacoes)
                .SetValidator(new CadastrarFormacaoCommandValidator())
                .When(t => t.Formacoes != null);
        }
    }

    public class CadastrarExperienciaCommandValidator : AbstractValidator<CadastrarCandidatoCommand.CadastrarExperienciaCommand>
    {
        public CadastrarExperienciaCommandValidator()
        {
            RuleFor(e => e.Empresa)
                    .NotEmpty().WithMessage("Empresa é obrigatória.")
                    .MaximumLength(150).WithMessage("Empresa não pode ter mais do que 150 caracteres!");

            RuleFor(e => e.Cargos)
                .NotEmpty().WithMessage("Cargo é obrigatório.")
                .Must(cargos => cargos.Any()).WithMessage("Deve haver pelo menos um cargo.");

            RuleForEach(e => e.Cargos).SetValidator(new CadastrarCargoCommandValidator());
        }
    }

    public class CadastrarCargoCommandValidator : AbstractValidator<CadastrarCandidatoCommand.CadastrarCargoCommand>
    {
        public CadastrarCargoCommandValidator()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório.")
                .MaximumLength(150).WithMessage("Título não pode ter mais do que 150 caracteres!");

            RuleFor(c => c.DataInicial)
                .NotEmpty().WithMessage("Data Inicial é obrigatória.")
                .LessThanOrEqualTo(c => c.DataFinal == default(DateTime) ? DateTime.MaxValue : c.DataFinal)
                .WithMessage("Data Inicial deve ser menor ou igual à Data Final.");

            RuleFor(c => c.Descricao)
                .MaximumLength(500).WithMessage("Descrição não pode ter mais do que 500 caracteres!");
        }
    }

    public class CadastrarFormacaoCommandValidator : AbstractValidator<CadastrarCandidatoCommand.CadastrarFormacaoCommand>
    {
        public CadastrarFormacaoCommandValidator()
        {
            When(f => f != null, () =>
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
            });
        }
    }
}
