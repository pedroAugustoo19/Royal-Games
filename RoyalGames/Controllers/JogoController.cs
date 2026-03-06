<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
=======
﻿using Microsoft.AspNetCore.Http;
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
<<<<<<< HEAD
using System.Security.Claims;
=======
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;
<<<<<<< HEAD

        public JogoController(JogoService service)
        {
            _service = service;
        }

        private int ObterUsuarioIdLogado()
        {
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idTexto))
            {
                throw new DomainException("Usuário não autenticado");
            }

            return int.Parse(idTexto);
        }

        [HttpGet]
        public ActionResult<List<LerJogoDto>> Listar()
        {
            List<LerJogoDto> jogos = _service.Listar();

            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerJogoDto> ObterPorId(int id)
        {
            LerJogoDto jogo = _service.ObterPorId(id);

            if (jogo == null)
            {
                return NotFound();
            }

            return Ok(jogo);
        }

        [HttpGet("{id}/imagem")]
        public ActionResult ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);

                return File(imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]

        public ActionResult Adicionar([FromForm] CriarJogoDto jogoDto)
        {
            try
            {
                int usuarioId = ObterUsuarioIdLogado();
                _service.Adicionar(jogoDto, usuarioId);
                return StatusCode(201);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Atualizar(int id, [FromForm] AtualizarJogoDto jogoDto)
        {
            try
            {
                _service.Atualizar(id, jogoDto);
                return NoContent();
            }
            catch (DomainException ex)
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
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
=======
>>>>>>> 728cd3bd01a76e008714cb052266bbc1ae2a28aa
    }
}
