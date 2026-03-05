using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo ObterPorId(int id);
        Jogo ObterPorNome(string nome);
        bool NomeExiste(string nome, int? jogoIdAtual = null);
        void Adicionar(Jogo jogo, List<int> plataformaIds, List<int> generoIds);
        void Atualizar(Jogo jogo, List<int> plataformaIds, List<int> generoIds);
        void Remover(int id);
        void Adicionar(CriarJogoDto jogo, string genero);
    }
}
