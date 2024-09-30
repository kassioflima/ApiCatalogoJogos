using ApiCatalogoJogos.Domain.Dtos.Jogos;
using ApiCatalogoJogos.Domain.Interfaces.Jogos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController(ILogger<JogosController> _logger, IJogoService _jogoService) : ControllerBase
    {
        /// <summary>
        /// Consulta paginada para obter os jogos cadastrados na API
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação.
        /// </remarks>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Retorna a lista de jogos.</response>
        /// <response code="204">Caso não haja jogos.</response>
        /// <response code="500">Erro interno de servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<JogoDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1,
                                              [FromQuery, Range(1, 50)] int tamanhoPagina = 5)
        {
            _logger.LogInformation($"Iniciando consulta paginada dejogos pagina {pagina} tamanho {tamanhoPagina}");
            var result = await _jogoService.Obter(pagina, tamanhoPagina);
            if (result?.Count() == 0)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        /// Obtem jogos por id
        /// </summary>
        /// <param name="idJogo"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JogoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [HttpGet("{idJogo}")]
        public async Task<IActionResult> Obter([FromRoute] Guid idJogo)
        {
            _logger.LogInformation($"Iniciando consulta de jogo pelo id: {idJogo}");
            var result = await _jogoService.Obter(idJogo);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> Inserir([FromBody] JogoInputDto jogo)
        {
            try
            {
                var result = await _jogoService.Inserir(jogo);
                return Created(Url?.Action(nameof(Obter)), result?.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir jogo");
                return UnprocessableEntity("Já existe um jogo com este nome para produtora.");
            }
        }

        [HttpPut("{idJogo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> Atualizar([FromRoute] Guid idJogo, [FromBody] JogoInputDto jogo)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogo);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar jogo.");
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{idJogo}/preco/{preco}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] decimal preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar preço do jogo.");
                return NotFound("Não existe este jogo");
            }
        }

        [HttpDelete("{idJogo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> Excluir([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Excluir(idJogo);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar jogo.");
                return NotFound();
            }
        }
    }
}
