using RoyalGames.Domains;
using System.Runtime.Serialization;

namespace RoyalGames.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> Listar();
        Genero ObterPorId(int id);
        bool NomeExiste(string nome, int? categoriaIdAtual = null);
        void Adicionar(Genero genero);
        void Atualizar(Genero genero);
        void Remover(int id);
    }
}
