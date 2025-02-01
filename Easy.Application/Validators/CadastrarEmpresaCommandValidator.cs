using Easy.Application.Commands.CadastrarEmpresa;
using FluentValidation;

namespace Easy.Application.Validators
{
    public class CadastrarEmpresaCommandValidator : AbstractValidator<CadastrarEmpresaCommand>
    {
        public CadastrarEmpresaCommandValidator()
        {
            RuleFor(t => t.Cnpj)
                .NotEmpty().WithMessage("CNPJ é obrigatório.")
                .Length(14).WithMessage("CNPJ deve conter exatamente 14 caracteres.")
                .Must(ValidarCnpj).WithMessage("CNPJ inválido.");

            RuleFor(t => t.NomeFantasia)
                .NotEmpty().WithMessage("Nome Fantasia é obrigatório.")
                .MaximumLength(150).WithMessage("Nome Fantasia não pode ter mais que 150 caracteres.");
        }

        private bool ValidarCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = cnpj.Trim();
            if (cnpj.Length != 14)
                return false;

            if (cnpj.All(c => c == cnpj[0]))
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador2[i];
            soma += digito1 * multiplicador2[12];
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith($"{digito1}{digito2}");
        }
    }
}
