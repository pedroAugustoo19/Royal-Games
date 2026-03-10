using RoyalGames.Domains;
namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo ObterPorId(int id);
        byte[] ObterImagem(int id);
        bool NomeExiste(string nome, int? jogoIdAtual = null);
        void Adicionar(Jogo jogo, List<int> generoIds, List<int> plataformaIds);
        void Atualizar(Jogo jogo, List<int> generoIds, List<int> plataformaIds);
        void Remover(int id);
    }
}
