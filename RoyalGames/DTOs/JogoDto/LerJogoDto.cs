namespace RoyalGames.DTOs.JogoDto
{
    public class LerJogoDto
    {
        public int JogoId { get; set; }
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;
    }
}
