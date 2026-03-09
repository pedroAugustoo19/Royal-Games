using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Royal_GamesContext _context;

        public UsuarioRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario? ObterPorId(int id)
        {
            Usuario? usuario = _context.Usuario.FirstOrDefault(u => u.UsuarioID == id);
            return usuario;
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(usuario => usuario.Email == email);
        }

        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(usuario => usuario.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(u => u.UsuarioID == usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Senha = usuario.Senha;

            _context.SaveChanges();
        }
        
        public void Remover (int id)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(u => u.UsuarioID == id);

            if(usuarioBanco == null)
            {
                return; 
            }

            _context.Usuario.Remove(usuarioBanco);
            _context.SaveChanges();
        }
    }
}
