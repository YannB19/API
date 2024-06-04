using System.Diagnostics;
using System.Net;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {

        private readonly ProjetwpfContext ProjetwpfContext;

        public HeroController(ProjetwpfContext ProjetwpfContext)
        {
            this.ProjetwpfContext = ProjetwpfContext;
        }


        // GET: api/<HeroController>
        [HttpGet("GetHeros")]
        public async Task<ActionResult<List<HeroDTO>>> Get()
        {
            var List = await ProjetwpfContext.Hero.Select(
                s => new HeroDTO
                {
                    Id = s.Id,
                    Nom = s.Nom,
                    Description = s.Description
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        // GET api/<HeroController>/5
        [HttpGet("GetUserById")]
        public async Task<ActionResult<HeroDTO>> GetHeroById(int Id)
        {
            HeroDTO Hero = await ProjetwpfContext.Hero.Select(s => new HeroDTO
            {
                Id = s.Id,
                Nom = s.Nom,
                Description = s.Description
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (Hero == null)
            {
                return NotFound();
            }
            else
            {
                return Hero;
            }
        }

        // POST api/<HeroController>
        [HttpPost("InsertHero")]
        public async Task<HttpStatusCode> InsertUser(HeroDTO Hero)
        {
            var entity = new Hero()
            {
                Id = Hero.Id,
                Nom = Hero.Nom,
                Description = Hero.Description
            };
            ProjetwpfContext.Hero.Add(entity);
            await ProjetwpfContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        // PUT api/<HeroController>/5
        [HttpPut("UpdateHero")]
        public async Task<HttpStatusCode> UpdateUser(HeroDTO Hero)
        {
            var entity = await ProjetwpfContext.Hero.FirstOrDefaultAsync(s => s.Id == Hero.Id);
            entity.Id = Hero.Id;
            entity.Nom = Hero.Nom;
            entity.Description = Hero.Description;
            await ProjetwpfContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        // DELETE api/<HeroController>/5
        [HttpDelete("DeleteHero/{Id}")]
        public async Task<HttpStatusCode> DeleteHero(int Id)
        {
            var entity = new Hero()
            {
                Id = Id
            };
            ProjetwpfContext.Hero.Attach(entity);
            ProjetwpfContext.Hero.Remove(entity);
            await ProjetwpfContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
