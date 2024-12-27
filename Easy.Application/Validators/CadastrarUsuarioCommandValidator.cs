using Easy.Application.Commands.CadastrarUsuario;
using FluentValidation;

namespace Easy.Application.Validators
{
    public class CadastrarUsuarioCommandValidator : AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidator()
        {
            RuleFor(t => t.Email).NotEmpty().EmailAddress().WithMessage("Email não válido!");

            RuleFor(usuario => usuario.Senha)
                           .NotEmpty().WithMessage("Senha é obrigatória.")
                           .Matches(@"^.*(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).+$")
                           .WithMessage("A senha deve conter pelo menos 8 digitos, sendo: uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
        }
    }
}
