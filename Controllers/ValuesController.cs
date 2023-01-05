using EFCore.Domain;
using EFCoreWebAPI.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCoreWebAPI.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public readonly HeroiContexto _context;
        public ValuesController(HeroiContexto context)
        {
            _context = context;
        }

        //FILTRAR -> api/heroi/filtro/{name}
        [HttpGet("filtro/{name}")]
        public ActionResult GetFiltro(string name)
        {
            var listHeroi = _context.Herois
                            .Where(h=> h.Name.Contains(name))
                            .ToList();
            return Ok(listHeroi);
        }

        // GET -> api/heroi
        [HttpGet]
        public ActionResult Get()
        {
           var listHeroi = _context.Herois.ToList();
           return Ok(listHeroi);
        } 

        //INSERT/Update -> api/heroi/nameHero
        [HttpGet("{nameHero}")]
        public ActionResult Get(int id) 
        {
            //var heroi = new Heroi { Name = nameHero };
            var heroi = _context.Herois
                            .Where(h => h.Id == id)
                            .FirstOrDefault();
            //_context.Herois.Add(heroi);
            _context.SaveChanges();
            return Ok();
        } 

        // POST api/heroi
        [HttpPost]
        public void Post([FromBody] Heroi heroi)
        {
            //var heroi = new Heroi { Name = value };
            _context.Herois.Add(heroi);
            _context.SaveChanges();
            
        }

        // PUT api/heroi/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/heroi/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var heroi  = _context.Herois
                            .Where( e => e.Id == id)
                            .Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
