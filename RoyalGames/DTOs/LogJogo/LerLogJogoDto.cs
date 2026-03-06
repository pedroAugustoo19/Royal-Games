namespace RoyalGames.DTOs.LogJogo
{
    public class LerLogJogoDto
    {
        public int LogId { get; set; }
<<<<<<< HEAD
        public int JogoId { get; set; }
        public string NomeAnterior { get; set; } = null!;
        public decimal PrecoAnterior { get; set; }
=======
        public int? JogoId { get; set; }
        public string? NomeAnterior { get; set; } = null!;
        public decimal? PrecoAnterior { get; set; }
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa
        public DateTime DataAlteracao { get; set; }
    }
}
