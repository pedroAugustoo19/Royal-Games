using RoyalGames.Domains;
<<<<<<< HEAD
using RoyalGames.DTOs.JogoDto;
=======
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo ObterPorId(int id);
        Jogo ObterPorNome(string nome);
        bool NomeExiste(string nome, int? jogoIdAtual = null);
<<<<<<< HEAD
        void Adicionar(Jogo jogo, List<int> plataformaIds, List<int> generoIds);
        void Atualizar(Jogo jogo, List<int> plataformaIds, List<int> generoIds);
        void Remover(int id);
        void Adicionar(CriarJogoDto jogo, string genero);
=======
        void Adicionar(Jogo jogo);
        void Atualizar(Jogo jogo);
        void Remover(int id);
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa
    }
}
