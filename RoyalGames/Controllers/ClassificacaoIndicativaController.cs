using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.ClassificacaoIndicativaDto;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoIndicativaController : ControllerBase
    {
        private readonly ClassificacaoIndicativaService _service;

        public ClassificacaoIndicativaController(ClassificacaoIndicativaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerClassificacaoIndicativaDto>> Listar()
        {
            List<LerClassificacaoIndicativaDto> classificacoes = _service.Listar();
            return Ok(classificacoes);
        }

        [HttpGet("{id}")]
        public ActionResult<LerClassificacaoIndicativaDto> ObterPorId(int id)
        {
            LerClassificacaoIndicativaDto classificacao = _service.ObterPorId(id);

            if(classificacao == null)
            {
                return NotFound();
            }

            return Ok(classificacao);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarClassificacaoIndicativaDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, CriarClassificacaoIndicativaDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return NoContent();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
