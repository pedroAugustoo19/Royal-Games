using RoyalGames.Applications.Conversoes;
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

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            List<LerJogoDto> jogosDto = jogos.Select(JogoParaDto.converterParaDto).ToList();

            return jogosDto;
        }

        public LerJogoDto ObterPorId(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo nao encontrado");
            }

            return JogoParaDto.converterParaDto(jogo);
        }

        private static void ValidarCadastro(CriarJogoDto produtoDto)
        {
            if (string.IsNullOrWhiteSpace(produtoDto.Nome))
            {
                throw new DomainException("Nome eh obrigatorio");
            }

            if (produtoDto.Preco < 0)
            {
                throw new DomainException("Preco deve ser maior que zero");
            }

            if (string.IsNullOrWhiteSpace(produtoDto.Descricao))
            {
                throw new DomainException("Descricao eh obrigatoria");
            }

            if (produtoDto.Imagem == null || produtoDto.Imagem.Length == 0)
            {
                throw new DomainException("Imagem eh obrigatoria");
            }

            if (produtoDto.PlataformaIds == null || produtoDto.PlataformaIds.Count == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma Plataforma");
            }

            if (produtoDto.GeneroIds == null || produtoDto.GeneroIds.Count == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma Genero");
            }
        }

        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _repository.ObterImagem(id);

            if (imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem nao encontrada");
            }

            return imagem;
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);

            if (_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo ja existente");
            }

            Jogo jogo = new Jogo
            {
                Nome = jogoDto.Nome,
                Preco = jogoDto.Preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                StatusJogo = true,
                UsuarioID = usuarioId,
                ClassificacaoIndicativaID = jogoDto.ClassificacaoIndicativaId
            };

            _repository.Adicionar(jogo, jogoDto.PlataformaIds, jogoDto.GeneroIds);

            return JogoParaDto.converterParaDto(jogo);
        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            Jogo jogoBanco = _repository.ObterPorId(id);

            if (jogoBanco == null)
            {
                throw new DomainException("Produto não encontrado!");
            }

            // produtoIdAtual: -> dois pontos serve para passar o valor do parametro
            if (_repository.NomeExiste(jogoDto.Nome, jogoIdAtual: id))
            {
                throw new DomainException("Já existe outro produto com esse nome!");
            }

            if (jogoDto.GeneroIds == null || jogoDto.GeneroIds.Count == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma categoria!");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero!");
            }

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Preco = jogoDto.Preco;
            jogoBanco.Descricao = jogoDto.Descricao;

            if (jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            if (jogoDto.StatusProduto.HasValue)
            {
                jogoBanco.StatusJogo = jogoDto.StatusProduto.Value;
            }

            _repository.Atualizar(jogoBanco, jogoDto.GeneroIds, jogoDto.PlataformaIds);

            return JogoParaDto.converterParaDto(jogoBanco);
        }

        public void Remover(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo nao encontrado");
            }

            _repository.Remover(id);
        }
    }
}
