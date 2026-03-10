using RoyalGames.Domains;
using RoyalGames.DTOs.GeneroDto;
using RoyalGames.DTOs.PlataformaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }

        public List<LerPlataformaDto> Listar()
        {
            List<Plataforma> plataformas = _repository.Listar();

            List<LerPlataformaDto> plataformaDto = plataformas.Select(plataforma => new LerPlataformaDto
            {
                PlataformaId = plataforma.PlataformaID,
                Nome = plataforma.Nome,
            }).ToList();

            return plataformaDto;

        }

        public LerPlataformaDto ObterPorId(int id)
        {
            Plataforma plataforma = _repository.ObterPorId(id);

            if (plataforma == null)
            {
                throw new DomainException("Plataforma nao encontrada");
            }

            LerPlataformaDto plataformaDto = new LerPlataformaDto
            {
                PlataformaId = plataforma.PlataformaID,
                Nome = plataforma.Nome,
            };

            return plataformaDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome eh obrigatorio");
            }
        }

        public void Adicionar (CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Plataforma ja existe");
            }

            Plataforma plataforma = new Plataforma
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(plataforma);
        }

        public void Atualizar (int id, CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if(plataformaBanco == null)
            {
                throw new DomainException("Plataforma nao encontrada");
            }

            if(_repository.NomeExiste(criarDto.Nome, categoriaIdAtual: id))
            {
                throw new DomainException("Ja existe outra plataforma com esse nome");
            }

            plataformaBanco.Nome = criarDto.Nome;
            _repository.Atualizar(plataformaBanco);
        }

        public void Remover (int id)
        {
            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if(plataformaBanco == null)
            {
                throw new DomainException("Plataforma nao encontrada");
            }

            _repository.Remover(id);
        }
    }
}
