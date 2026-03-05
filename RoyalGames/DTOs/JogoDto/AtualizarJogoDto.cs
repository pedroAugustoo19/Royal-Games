using RoyalGames.Domains;

namespace RoyalGames.DTOs.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!; // A imagem vem via multipart/form-data, ideal para upload de arquivo

        public string Plataforma { get; set; } = null!;

        public string Genero { get; set; } = null!;

        public bool? StatusJogo{ get; set; }


        public List<Plataforma> plataformaIds { get; set; } = new List<Plataforma>();
    }
}
