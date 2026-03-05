using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IClassificacaoIndicativaRepository
    {
        List<ClassificacaoIndicativa> Listar();
        ClassificacaoIndicativa ObterPorId(int id);
        bool NomeExiste(string nome, int? categoriaIdAtual = null);
        void Adicionar(ClassificacaoIndicativa classificacaoIndicativa);
        void Atualizar(ClassificacaoIndicativa classificacao);
        void Remover(int id);
    }
}
