using EFCore.Domain;
using EFCore.Repo;
using EFCoreWebAPI.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhasController : ControllerBase
    {
        public readonly IEFCoreRepository _repo;
        public BatalhasController(IEFCoreRepository repo)
        {
            _repo = repo;
        }
        
       
        // GET: api/<BatalhasController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                
                var batalhas = await _repo.GetAllBatalhas(true);
                return Ok(batalhas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        //// GET api/<BatalhasController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id, true);
                return Ok(batalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Batalha value)
        {
            try
            {
                _repo.Add(value);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok("Batalha cadastrada com sucesos!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não cadastrado");
        }

        //PUT api/<BatalhasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,Batalha model)
        {
            var batalha = await _repo.GetBatalhaById(id);
            try
            {
                if (batalha.Id != null)
                {
                    _repo.Update(model);
                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok("Batalha atualizada com  sucesso!");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return Ok("Batalha não encontrada!");
        }

        // DELETE api/<BatalhasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);
                if (await _repo.SaveChangesAsync())
                {
                    _repo.Delete(batalha);
                    return Ok("Batalha deletada com sucesso com sucesos!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não cadastrado");
        }
    }
}
