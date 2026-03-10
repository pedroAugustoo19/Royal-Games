using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Applications.Conversoes
{
    public class JogoParaDto
    {
        public static LerJogoDto converterParaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoID,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusJogo = jogo.StatusJogo,

                GeneroIds = jogo.Genero.Select(categoria => categoria.GeneroID).ToList(),

                Generos = jogo.Genero.Select(categoria => categoria.Nome).ToList(),


                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email

            };
        }
    }
}
