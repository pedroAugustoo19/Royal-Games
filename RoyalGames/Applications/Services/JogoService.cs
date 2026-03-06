using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Repository;
using RoyalGames.Applications.Regras;
using RoyalGames.Applications.Conversoes;

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

            if (jogoDto.PlataformaId == null)
            {
                throw new DomainException("Plataforma é obrigatória.");
            }

            if (jogoDto.GeneroId == null)
            {
                throw new DomainException("Genero é obrigatório.");
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

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int jogoId)
        {
            ValidarJogo(jogoDto);

            if (_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo já existente.");
            }

            Jogo jogo = new Jogo
            {
                Nome = jogoDto.Nome,
                Preco = jogoDto.Preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                Plataforma = jogoDto.Plataforma,
                Genero = jogoDto.Genero
            };

            _repository.Adicionar(jogo);

            return JogoParaDto.ConverterParaDto(jogo);
        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorId(id);

            if (jogoBanco == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            if (_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Já existe um jogo com esse nome");
            }

            if (jogoDto.Descricao == null)
            {
                throw new DomainException("O jogo deve ter uma descrição.");
            }

            if (jogoDto.Nome == null)
            {
                throw new DomainException("O jogo deve ter um nome.");
            }

            if (jogoDto.Genero == null)
            {
                throw new DomainException("O jogo deve ter ao menos um gênero.");
            }

            if (jogoDto.Plataforma == null)
            {
                throw new DomainException("O jogo deve ter ao menos uma plataforma.");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("O preço do jogo deve ser maior ou igual a zero.");
            }

            if (jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            _repository.Atualizar(jogoBanco, jogoDto.plataformaIds);


            return JogoParaDto.ConverterParaDto(jogoBanco);
        }

        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _repository.ObterImagem(id);

            if (imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem não encontrada");
            }

            return imagem;
        }

        public void Remover(int id)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            _repository.Remover(id);
        }


    }
}
