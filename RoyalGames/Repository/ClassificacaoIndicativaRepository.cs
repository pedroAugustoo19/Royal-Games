using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repository
{
    public class ClassificacaoIndicativaRepository : IClassificacaoIndicativaRepository
    {
        private readonly Royal_GamesContext _context;

        public ClassificacaoIndicativaRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _context.ClassificacaoIndicativa.ToList();
        }

        public ClassificacaoIndicativa ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacaoIndicativa = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoIndicativaID == id);
            return classificacaoIndicativa;
        }

        public bool NomeExiste(string nome, int? classificacaoIdAtual = null)
        {
            var consulta = _context.ClassificacaoIndicativa.AsQueryable();

            if(classificacaoIdAtual.HasValue)
            {
                consulta = consulta.Where(classificacaoIndicativa => classificacaoIndicativa.ClassificacaoIndicativaID != classificacaoIdAtual.Value);
            }
            return consulta.Any(c => c.Nome == nome);
        }

        public void Adicionar(ClassificacaoIndicativa classificacaoIndicativa)
        {
            _context.ClassificacaoIndicativa.Add(classificacaoIndicativa);

            _context.SaveChanges();
        }

        public void Atualizar (ClassificacaoIndicativa classificacaoIndicativa)
        {
            ClassificacaoIndicativa classificacaoIndicativaBanco = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoIndicativaID == classificacaoIndicativa.ClassificacaoIndicativaID);

            if(classificacaoIndicativaBanco == null)
            {
                return;
            }

            classificacaoIndicativaBanco.Nome = classificacaoIndicativa.Nome;
            _context.SaveChanges();

        }

        public void Remover (int id )
        {
            ClassificacaoIndicativa classificacaoIndicativaBanco = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoIndicativaID == id);

            if (classificacaoIndicativaBanco == null)
            {
                return;
            }

            _context.ClassificacaoIndicativa.Remove(classificacaoIndicativaBanco);
            _context.SaveChanges();
        }
    }
}
