using RoyalGames.Domains;
namespace RoyalGames.DTOs.JogoDto
{
    public class LerJogoDto
    {

        public int JogoID { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile Imagem { get; set; } = null!;
        public bool? StatusJogo { get; set; }

        // Generos
        public List<int> GeneroIds { get; set; } = new();
        public List<string> Generos { get; set; } = new();

        // Plataforma
        public List<int> PlataformaIds { get; set; } = new();
        public List<string> Plataforma { get; set; } = new();

        // usuario que cadastrou
        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }
    }
}