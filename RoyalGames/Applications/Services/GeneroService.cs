using RoyalGames.Domains;
using RoyalGames.DTOs.GeneroDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Repository;

namespace RoyalGames.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> generos = _repository.Listar();

            List<LerGeneroDto> generoDto = generos.Select(genero => new LerGeneroDto
            {
                GeneroId = genero.GeneroID,
                Nome = genero.Nome,
            }).ToList();

            return generoDto;
        }

        public LerGeneroDto ObterPorId(int id)
        {
            Genero genero = _repository.ObterPorId(id);

            if(genero == null)
            {
                throw new DomainException("Genero nao encontrado");
            }

            LerGeneroDto generoDto = new LerGeneroDto
            {
                GeneroId = genero.GeneroID,
                Nome = genero.Nome,
            };
            
            return generoDto;   
        }
        
        public static void ValidarNome (string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome eh obrigatorio");
            }
        }

        public void Adicionar(CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);
            
            if(_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Nome ja existe");
            }

            Genero genero = new Genero
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(genero);
        }

        public void Atualizar (int id, CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            Genero generoBanco = _repository.ObterPorId(id);
            
            if(generoBanco == null)
            {
                throw new DomainException("Genero nao encontrado");
            }

            if(_repository.NomeExiste(criarDto.Nome, categoriaIdAtual: id))
            {
                throw new DomainException("Ja existe outro genero com esse nome");
            }

            generoBanco.Nome = criarDto.Nome;
            _repository.Atualizar(generoBanco);
        }

        public void Remover (int id)
        {
            Genero generoBanco = _repository.ObterPorId(id);

            if(generoBanco == null)
            {
                throw new DomainException("Genero nao encontrado");
            }

            _repository.Remover(id);
        }

    }
}
