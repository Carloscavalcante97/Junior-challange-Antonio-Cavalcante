using Junior_challange_.Services;
using Junior_Challenge.Communication.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace Junior_Challenge.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnelController : ControllerBase
{
    private readonly ServicosAnel _servicosAnel;
    public AnelController(ServicosAnel servicosAnel)
    {
        _servicosAnel = servicosAnel;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Anel>), StatusCodes.Status200OK)]
    public ActionResult<List<Anel>> GetAllRings(Guid? id)
    {
        if (id.HasValue)
        {
            var anel = _servicosAnel.GetRingById((Guid)id);
            if (anel == null)
            {
                return NoContent();
            }
            return Ok(new List<Anel> { anel });
        }
        var aneis = _servicosAnel.ListarTodosAneis();

        if (aneis.Count == 0)
        {
            return NoContent();
        }
        return Ok(aneis);
    }




    [HttpPost]
    [ProducesResponseType(typeof(Anel), StatusCodes.Status201Created)]
    public IActionResult CreateRing(Anel anel)
    {
        try
        {
            _servicosAnel.CriarAnel(anel);
            return CreatedAtAction(nameof(CreateRing), anel);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar anel: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRing(Guid id, Anel anel)
    {
        try
        {
            var existingAnel = _servicosAnel.GetRingById(id);
            if (existingAnel == null)
            {
                return NotFound($"Anel com id {id} não encontrado.");
            }

            _servicosAnel.AtualizarAnel(anel);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteRing(Guid id)
    {
        try
        {
            _servicosAnel.deletarAnel(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}