using RoyalGames.Domains;
using RoyalGames.DTOs.ClassificacaoIndicativaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativaRepository _repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativaRepository repository)
        {
            _repository = repository;
        }

        public List<LerClassificacaoIndicativaDto> Listar()
        {
            List<ClassificacaoIndicativa> classificacaos = _repository.Listar();

            List<LerClassificacaoIndicativaDto> classificacaoDto = classificacaos.Select(classificacao => new LerClassificacaoIndicativaDto
            {
                ClassificacaoIndicativaID = classificacao.ClassificacaoIndicativaID,
                Nome = classificacao.Nome,
            }).ToList();

            return classificacaoDto;

        }

        public LerClassificacaoIndicativaDto ObterPorId (int id)
        {
            ClassificacaoIndicativa classificacao = _repository.ObterPorId(id);

            if(classificacao == null)
            {
                throw new DomainException("Categoria nao encontrada.");
            }

            LerClassificacaoIndicativaDto classificacaoDto = new LerClassificacaoIndicativaDto
            {
                ClassificacaoIndicativaID = classificacao.ClassificacaoIndicativaID,
                Nome = classificacao.Nome
            };

            return classificacaoDto;
        }

        private static void ValidarNome (string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome eh obrigatorio.");
            }
        }

        public void Adicionar(CriarClassificacaoIndicativaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if(_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Categoria ja existe");
            }

            ClassificacaoIndicativa classificacao = new ClassificacaoIndicativa
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(classificacao);
        }

        public void Atualizar(int id, CriarClassificacaoIndicativaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            ClassificacaoIndicativa classificacaoBanco = _repository.ObterPorId(id);

            if (classificacaoBanco == null)
            {
                throw new DomainException("Categoria nao encontrada");
            }

            if (_repository.NomeExiste(criarDto.Nome, categoriaIdAtual: id))
            {
                throw new DomainException("Ja existe outra categoria com esse nome");
            }

            classificacaoBanco.Nome = criarDto.Nome;
            _repository.Atualizar(classificacaoBanco);
        }

        public void Remover(int id)
        {
            ClassificacaoIndicativa classificacaoBanco = _repository.ObterPorId(id);

            if(classificacaoBanco == null)
            {
                throw new DomainException("Classificacao nao encontrada");
            }

            _repository.Remover(id);
        }
    }
}
