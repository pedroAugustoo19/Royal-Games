using RoyalGames.Domains;
using RoyalGames.DTOs.LogJogo;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService (ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogo = logs.Select(log => new LerLogJogoDto
            {
                LogId = log.Log_AlteracaoJogoID,
                JogoId = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao,
            }).ToList();    

            return listaLogJogo;
        }

        public List<LerLogJogoDto> ListarPorJogo(int jogoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(jogoId);

            List<LerLogJogoDto> listaLogJogo = logs.Select(log => new LerLogJogoDto
            {
                LogId = log.Log_AlteracaoJogoID,
                JogoId = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao,
            }).ToList();

            return listaLogJogo;
        }
    }
}
