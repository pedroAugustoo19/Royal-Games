using RoyalGames.Domains;

namespace RoyalGames.DTOs.JogoDto
{
    public class LerJogoDto
    {
        public int JogoId { get; set; }
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;
        
        public List<Genero> Genero { get; set; } = new List<Genero>();

        public List<Plataforma> Plataforma { get; set; } = new List<Plataforma>();

        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }
    }
}
