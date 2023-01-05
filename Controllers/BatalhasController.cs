using EFCore.Domain;
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

        public readonly HeroiContexto _context;
        public BatalhasController(HeroiContexto context)
        {
            _context = context;
        }
        // GET: api/<BatalhasController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Batalhas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro{ex}");
            }
        }

        // GET api/<BatalhasController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public ActionResult Get(int id)
        {
            try
            {
                var batalha = _context.Batalhas.Where(x => x.Id == id);
                return Ok(batalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhasController>
        [HttpPost]
        public void Post([FromBody] Batalha value)
        {
            try
            {
                _context.Batalhas.Add(value);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<BatalhasController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha value)
        {
            try
            {
                if (_context
                    .Batalhas
                    .AsNoTracking()
                    .FirstOrDefault(h=> h.Id == id) != null)
                {
                    _context.Batalhas.Update(value);
                    _context.SaveChanges();
                    return Ok("Alteração realizada com sucesso!");
                }
                return Ok("Batalha não encontrada!");  

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro{ex}");
            }
        }

        // DELETE api/<BatalhasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var batalha = _context.Batalhas
                            .Where(e => e.Id == id)
                            .Single();
            _context.Batalhas.Remove(batalha);
            _context.SaveChanges();
        }
    }
}
