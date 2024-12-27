namespace Easy.Application.Interfaces
{
    public interface IJwtService
    {
        string? ExtrairIdUsuario(string authorizationHeader);
    }
}
