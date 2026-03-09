using RoyalGames.Domains;
using RoyalGames.DTOs.UsuarioDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace RoyalGames.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto lerUsuario = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? true
            };

            return lerUsuario;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            
            List<LerUsuarioDto> usuarioDtos = usuarios
                .Select(usuarioBanco => LerDto(usuarioBanco))
                .ToList();

            return usuarioDtos;
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email invalido.");
            }
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha)) //garante que a senha nao seja vazia
            {
                throw new DomainException("Senha eh obrigatoria");
            }

            using var sha256 = SHA256.Create(); //gera um hash SHA256 e devolve em byte[]
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);

            if(usuario == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario? usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao encontrado.");
            }
            return LerDto(usuario);
        }
        public LerUsuarioDto Adicionar(CriarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);
            ValidarNome(usuarioDto.Nome);

            if(_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Ja existe um usuario com esse email");
            }

            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = HashSenha(usuarioDto.Senha),
                StatusUsuario = true
            };

            _repository.Adicionar(usuario);

            return LerDto (usuario);
        }

        public LerUsuarioDto Atualizar(int id, CriarUsuarioDto usuarioDto)
        {
            Usuario? usuarioBanco = _repository.ObterPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            ValidarEmail (usuarioDto.Email);

            Usuario? usuarioComMesmoEmail = _repository.ObterPorEmail(usuarioDto.Email);

            if(usuarioComMesmoEmail != null && usuarioComMesmoEmail.UsuarioID != id)
            {
                throw new DomainException("Ja existe um Usuario com esse email.");
            }

            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Email = usuarioDto.Email;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);

            _repository.Atualizar(usuarioBanco);

            return LerDto(usuarioBanco);
        }
        public void Remover(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);

            if(usuario == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            _repository.Remover(id);
        }
    }
}