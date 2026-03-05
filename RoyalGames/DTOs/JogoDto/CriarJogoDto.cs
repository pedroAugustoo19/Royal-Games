using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace RoyalGames.DTOs.JogoDto
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!; // A imagem vem via multipart/form-data, ideal para upload de arquivo

        public string Plataforma { get; set; } = null!;

        public string Genero { get; set; } = null!;
    }
}
