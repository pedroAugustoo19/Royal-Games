using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repository
{
    public class JogoRepository : IJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public JogoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _context.Jogo
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Plataforma)
                .ToList();

            return jogos;
        }

        public Jogo? ObterPorId(int id)
        {
            return _context.Jogo
                .Include(j => j.ClassificacaoIndicativa)
                .Include(j => j.Genero)
                .Include(j => j.Plataforma)
                .FirstOrDefault(j => j.JogoID == id);
        }

        public Jogo? ObterPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome)) return null;

            return _context.Jogo
                .Include(j => j.ClassificacaoIndicativa)
                .Include(j => j.Genero)
                .Include(j => j.Plataforma)
                .FirstOrDefault(j => j.Nome == nome);
        }     
        

        public bool NomeExiste(string nome, int? jogoIdAtual = null)
        {
            var nomeExiste = _context.Jogo.AsQueryable();

            if(jogoIdAtual.HasValue)
            {
                nomeExiste = nomeExiste.Where(j => j.JogoID != jogoIdAtual.Value);
            }

            return nomeExiste.Any(j => j.Nome == nome);
        }

        public void Adicionar(Jogo jogo, List<int> generoId, List<int> plataformaId)
        {
            List<Genero> generos = _context.Genero
                .Where(g => generoId.Contains(g.GeneroID))
                .ToList();

            jogo.Genero = generos;

            List<Plataforma> plataformas = _context.Plataforma
                .Where(p => plataformaId.Contains(g.PlataformaID))
                .ToList();

            jogo.Plataforma = plataformas;


            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo)
        {
            Jogo? jogoBanco = _context.Jogo.FirstOrDefault(j => j.JogoID == jogo.JogoID);

            if (jogo == null)
            {
                return;
            }

            jogo.Nome = jogo.Nome;

            _context.SaveChanges();
        }

        void Remover(int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(jogo => jogo.JogoID == id);

            if(jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }
    }
}
