using Easy.Application.Interfaces;
using Easy.Core.Entities;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Easy.Application.Commands.SalvarDadosCandidato
{
    public class SalvarDadosCandidatoCommandHandler : IRequestHandler<SalvarDadosCandidatoCommand, Result>
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;

        public SalvarDadosCandidatoCommandHandler(ICandidatoRepository candidatoRepository, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _candidatoRepository = candidatoRepository;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
        }

        public async Task<Result> Handle(SalvarDadosCandidatoCommand request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            var idUsuario = _jwtService.ExtrairIdUsuario(authorizationHeader);

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result.Fail("Usuário não identificado no token.");
            }

            if (string.IsNullOrEmpty(request.UrlPublica))
            {
                return Result.Fail("A URL do perfil não pode ser vazia.");
            }

            var listaExperiencias = request.Experiencias?
                .Select(experiencia =>
                {
                    var listaCargos = experiencia.Cargos?
                        .Select(cargo =>
                        {
                            if (!string.IsNullOrEmpty(cargo.Periodo))
                            {
                                StringHelpers.ExtrairDatasLinkedin(cargo.Periodo, out DateTime dataInicial, out DateTime dataFinal);

                                return new Candidato.Cargo(
                                    cargo.Titulo!,
                                    dataInicial == DateTime.MinValue ? null : dataInicial,
                                    dataFinal == DateTime.MinValue ? null : dataFinal,
                                    cargo.Descricao!
                                );
                            }
                            return null;
                        })
                        .Where(cargo => cargo != null)
                        .ToList();

                    return new Candidato.Experiencia(
                        experiencia.Empresa!,
                        experiencia.Local!,
                        listaCargos
                    );
                })
                .Where(experiencia => experiencia != null)
                .ToList();

            var listaFormacoes = request.Formacoes?
                .Select(formacao =>
                {
                    if (!string.IsNullOrEmpty(formacao.Periodo))
                    {
                        StringHelpers.ExtrairDatasLinkedin(formacao.Periodo, out DateTime dataInicial, out DateTime dataFinal);
                        return new Candidato.Formacao(
                            formacao.Instituicao,
                            formacao.Curso,
                            dataInicial == DateTime.MinValue ? null : dataInicial,
                            dataFinal == DateTime.MinValue ? null : dataFinal
                        );
                    }

                    return null;
                })
                .Where(formacao => formacao != null)
                .ToList();

            byte[]? imageBytes = null;

            if (!string.IsNullOrEmpty(request.Foto))
            {
                string base64String = request.Foto;

                if (base64String.StartsWith("data:image"))
                {
                    base64String = base64String.Substring(base64String.IndexOf(",") + 1);
                }

                imageBytes = Convert.FromBase64String(base64String);
            }

            var candidato = new Candidato(idUsuario, request.UrlPublica, request.Nome, request.DescricaoProfissional, imageBytes, request.Sobre?.Replace("<!---->", ""), listaExperiencias, listaFormacoes);

            await _candidatoRepository.CadastrarAssincrono(candidato);

            return Result.Ok();
        }
    }
}
