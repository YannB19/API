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
    public class FilmController : ControllerBase
    {
        
        private readonly ProjetwpfContext ProjetwpfContext;

        public FilmController(ProjetwpfContext ProjetwpfContext)
        {
            this.ProjetwpfContext = ProjetwpfContext;
        }
        

        // GET: api/<FilmController>
        [HttpGet("GetFilms")]
        public async Task<ActionResult<List<FilmDTO>>> Get()
        {
            var List = await ProjetwpfContext.Film.Select(
                s => new FilmDTO
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

        // GET api/<FilmController>/5
        [HttpGet("GetFilmById")]
        public async Task<ActionResult<FilmDTO>> GetFilmById(int Id)
        {
            FilmDTO Film = await ProjetwpfContext.Film.Select(s => new FilmDTO {
                Id = s.Id,
                    Nom = s.Nom,
                    Description = s.Description
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (Film == null)
            {
                return NotFound();
            }
            else
            {
                return Film;
            }
        }

        // POST api/<FilmController>
        [HttpPost("InsertFilm")]
        public async Task<HttpStatusCode> InsertFilm(FilmDTO Film)
        {
            var entity = new Film()
            {
                Id = Film.Id,
                Nom = Film.Nom,
                Description = Film.Description
            };
            ProjetwpfContext.Film.Add(entity);
            await ProjetwpfContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        // PUT api/<FilmController>/5
        [HttpPut("UpdateFilm")]
        public async Task<HttpStatusCode> UpdateFilm(FilmDTO Film)
        {
            var entity = await ProjetwpfContext.Film.FirstOrDefaultAsync(s => s.Id == Film.Id);
            entity.Id = Film.Id;
            entity.Nom = Film.Nom;
            entity.Description = Film.Description;
            await ProjetwpfContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        // DELETE api/<FilmController>/5
        [HttpDelete("DeleteFilm/{Id}")]
        public async Task<HttpStatusCode> DeleteFilm(int Id)
        {
            var entity = new Film()
            {
                Id = Id
            };
            ProjetwpfContext.Film.Attach(entity);
            ProjetwpfContext.Film.Remove(entity);
            await ProjetwpfContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
