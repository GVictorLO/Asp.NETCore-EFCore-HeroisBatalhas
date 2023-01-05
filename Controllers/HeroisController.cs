using EFCore.Domain;
using EFCore.Repo;
using EFCoreWebAPI.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroisController : ControllerBase
    {
        public readonly IEFCoreRepository _repo;
        public HeroisController(IEFCoreRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<HeroisController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                bool incluirBatalha = false;
                var herois = _repo.GetAllHerois(incluirBatalha);
                return Ok(await herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        //// GET api/<HeroisController>/5
        [HttpGet("{id}", Name = "GetHeroi")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(id, true);
                return Ok(heroi);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<HeroisController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Heroi value)
        {
            try
            {
                _repo.Add(value);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok("Heroi cadastrada com sucesos!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não cadastrado");
        }

        //PUT api/<HeroisController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            try
            {
                var heroi = _repo.GetHeroiById(id);
                if (heroi.Id != null)
                {
                    _repo.Update(heroi);
                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok("Alteração realizada com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Heroi não encontrado");
        }

        // DELETE api/<HeroisController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi =await _repo.GetHeroiById(id);
                if (heroi.Id != null)
                {
                    _repo.Delete(heroi);
                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok("Heroi deletado com sucesso!");
                    } 
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Heroi não encontrado");
        }
    }
}
