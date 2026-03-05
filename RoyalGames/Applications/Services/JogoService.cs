using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Repository;

namespace RoyalGames.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;
        public JogoService(JogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            List<LerJogoDto> jogoDto = jogos.Select(jogo => new LerJogoDto
            {
                JogoId = jogo.JogoID,
                Nome = jogo.Nome
            }).ToList();

            return jogoDto;
        }

        public LerJogoDto ObterPorId(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("ID do jogo não encontrado.");
            }

            LerJogoDto jogoDto = new LerJogoDto
            {
                JogoId = jogo.JogoID,
                Nome = jogo.Nome
            };

            return jogoDto;
        }

        public LerJogoDto ObterPorNome(string nome)
        {
            Jogo jogo = _repository.ObterPorNome(nome);

            if (jogo == null)
            {
                throw new DomainException("Nome do jogo não encontrado.");
            }

            LerJogoDto jogoDto = new LerJogoDto
            {
                JogoId = jogo.JogoID,
                Nome = jogo.Nome
            };

            return jogoDto;
        }

        private static void ValidarJogo(CriarJogoDto jogoDto)
        {
            if (string.IsNullOrWhiteSpace(jogoDto.Nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            if (string.IsNullOrWhiteSpace(jogoDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(jogoDto.Plataforma))
            {
                throw new DomainException("Plataforma é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(jogoDto.Genero))
            {
                throw new DomainException("Descrição é obrigatória.");
            }

            if (jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Imagem é obrigatória.");
            }

        }
        private static void NomeExiste(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        void Adicionar(CriarJogoDto criarJogoDto)
        {
            ValidarJogo(criarJogoDto);

            if (_repository.NomeExiste(criarJogoDto.Nome))
            {
                throw new DomainException("Promoção já existente.");
            }

            criarJogoDto promocao = new Promocao
            {
                Nome

                Preco

                Descricao
                Imagem 
                Plataforma 

                Genero 
    };

            _repository.Adicionar(promocao);
        }
        void Atualizar(Jogo jogo);
        void Remover(int id);
    }
}
