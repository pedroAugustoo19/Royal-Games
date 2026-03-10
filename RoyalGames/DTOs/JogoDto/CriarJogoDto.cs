using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoyalGames.Domains;
using static System.Net.Mime.MediaTypeNames;

namespace RoyalGames.DTOs.JogoDto
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!; // A imagem vem via multipart/form-data, ideal para upload de arquivo

        public List<int> GeneroIds { get; set; } = new List<int>();
        public int GeneroId { get; set; }

        public List<int> PlataformaIds { get; set; } = new List<int>();
        public int PlataformaId { get; set; }

        public List<ClassificacaoIndicativa> ClassificacaoIds { get; set; } = new List<ClassificacaoIndicativa>();
        public int ClassificacaoId { get;  set; }
    }
}
