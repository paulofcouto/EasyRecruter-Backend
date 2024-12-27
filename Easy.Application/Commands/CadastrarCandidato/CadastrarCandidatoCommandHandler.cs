using Easy.Core.Entities;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Easy.Application.Commands.CadastrarCandidato
{
    public class CadastrarCandidatoCommandHandler : IRequestHandler<CadastrarCandidatoCommand, Result>
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CadastrarCandidatoCommandHandler(ICandidatoRepository candidatoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _candidatoRepository = candidatoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> Handle(CadastrarCandidatoCommand request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Result.Fail("Token JWT não encontrado.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idUsuario = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result.Fail("Usuário não identificado no token.");
            }

            var listaExperiencias = new List<Candidato.Experiencia>();

            foreach (var experiencia in request.Experiencias)
            {
                var listaCargos = new List<Candidato.Cargo>();

                foreach (var cargo in experiencia.Cargos)
                {
                    var cargoCandidato = new Candidato.Cargo(cargo.Titulo, cargo.DataInicial, cargo.DataFinal, cargo.Descricao);

                    listaCargos.Add(cargoCandidato);
                }

                var expecienciaCandidato = new Candidato.Experiencia(experiencia.Empresa, experiencia.Local, listaCargos);

                listaExperiencias.Add(expecienciaCandidato);
            }

            var listaFormacoes = new List<Candidato.Formacao>();

            foreach (var formacao in request.Formacoes)
            {
                var formacaoCandidato = new Candidato.Formacao(formacao.Instituicao, formacao.Curso, formacao.DataInicial, formacao.DataDeConclusao);

                listaFormacoes.Add(formacaoCandidato);

            }

            var candidato = new Candidato(idUsuario, request.UrlPublica, request.Nome, request.DescricaoProfissional, null, request.Sobre.Replace("<!---->", ""), listaExperiencias, listaFormacoes);

            await _candidatoRepository.CadastrarAssincrono(candidato);

            return Result.Ok();
        }
    }
}
