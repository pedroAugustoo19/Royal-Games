using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo ObterPorId(int id);
        Jogo ObterPorNome(string nome);
        bool NomeExiste(string nome, int? jogoIdAtual = null);
        void Adicionar(Jogo jogo);
        void Atualizar(Jogo jogo);
        void Remover(int id);
    }
}
