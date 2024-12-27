using Easy.Core.Enum;

namespace Easy.Core.Entities
{
    public class Usuario : BaseEntity
    {
        public Usuario(string email, string senhaHash)
        {
            Email = email;
            SenhaHash = senhaHash;
            Nome =  String.Empty;
            Tipo = TipoUsuario.Free;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public TipoUsuario Tipo { get; private set; }

        //ToDo
        public void EditarNome(string nome)
        {
            Nome = nome;
        }

        //ToDo
        public void AlterarSenha(string senhaHash)
        {
            SenhaHash = senhaHash;
        }

        //ToDo
        public void AlterarTipo(TipoUsuario tipo)
        {
            Tipo = tipo;
        }
    }
}
