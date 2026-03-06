using Microsoft.EntityFrameworkCore.Metadata.Internal;
<<<<<<< HEAD
using RoyalGames.Domains;
=======
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa
using static System.Net.Mime.MediaTypeNames;

namespace RoyalGames.DTOs.JogoDto
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!; // A imagem vem via multipart/form-data, ideal para upload de arquivo

<<<<<<< HEAD
        public List<Genero> Genero { get; set; } = new List<Genero>();
        public int GeneroId { get; set; }

        public List<Plataforma> Plataforma { get; set; } = new List<Plataforma>();
        public int PlataformaId { get; set; }

    }

=======
        public string Plataforma { get; set; } = null!;

        public string Genero { get; set; } = null!;
    }
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa
}
