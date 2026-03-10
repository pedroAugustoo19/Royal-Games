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
                .Include(jogo => jogo.Usuario)
                .ToList();

            return jogos;
        }

        public Jogo ObterPorId(int id)
        {
            Jogo? jogo = _context.Jogo
                .Include(jogoDb => jogoDb.ClassificacaoIndicativa)
                .Include(jogoDb => jogoDb.Usuario)

                .FirstOrDefault(jogoDb => jogoDb.JogoID == id);

            return jogo;
        }

        public bool NomeExiste(string nome, int? jogoIdAtual = null)
        {
            var jogoConsultado = _context.Jogo.AsQueryable();

            if (jogoIdAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(jogo => jogo.JogoID != jogoIdAtual.Value);
            }

            return jogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            var jogo = _context.Jogo
                .Where(jogo => jogo.JogoID == id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return jogo;
        }


        public void Adicionar(Jogo jogo, List<int> generoIds, List<int> plataformaIds)
        {
            List<Genero> generos = _context.Genero.Where(generoAux => generoIds.Contains(generoAux.GeneroID)).ToList();
            List<Plataforma> plataformas = _context.Plataforma.Where(plataformaAux => plataformaIds.Contains(plataformaAux.PlataformaID)).ToList();

            jogo.Genero = generos;
            jogo.Plataforma = plataformas;

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> generoIds, List<int> plataformaIds)
        {

            Jogo? jogoAtualizar = _context.Jogo
                .Include(jogoAux => jogoAux.Genero)
                .Include(jogoAux => jogoAux.Plataforma)
                .Include(jogoAux => jogoAux.ClassificacaoIndicativaID)
                .FirstOrDefault();

            if(jogoAtualizar == null)
            {
                return;
            }

            jogoAtualizar.Nome = jogo.Nome;
            jogoAtualizar.Descricao = jogo.Descricao;
            jogoAtualizar.ClassificacaoIndicativaID = jogo.ClassificacaoIndicativaID;

            if (jogoAtualizar.Imagem != null || jogoAtualizar.Imagem.Length > 0)
                jogoAtualizar.Imagem = jogo.Imagem;

            var jogoPlataforma = _context.Plataforma.Where(plataformasAux => plataformaIds.Contains(plataformasAux.PlataformaID)).ToList();
            jogoAtualizar.Plataforma.Clear();

            foreach(var plataformaFor in jogoPlataforma)
                jogoAtualizar.Plataforma.Add(plataformaFor);

            var jogoGenero = _context.Genero.Where(generoAux => generoIds.Contains(generoAux.GeneroID)).ToList();
            jogoAtualizar.Genero.Clear();

            foreach (var generoFor in jogoGenero)
                jogoAtualizar.Genero.Add(generoFor);

            _context.SaveChanges();

        }

        public void Remover (int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(j => j.JogoID == id);

            if (jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }

    }
}
