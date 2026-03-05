namespace RoyalGames.DTOs.LogJogo
{
    public class LerLogJogoDto
    {
        public int LogId { get; set; }
        public int JogoId { get; set; }
        public string NomeAnterior { get; set; } = null!;
        public decimal PrecoAnterior { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
