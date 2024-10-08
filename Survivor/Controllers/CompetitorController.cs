using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Context;
using Survivor.Entities;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorController : ControllerBase
    {
        private readonly SurvivorDbContext _context;// database ile bağlantı yapılıyor

        public CompetitorController(SurvivorDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetBy()
        {
            var get = await _context.Competitors.ToListAsync();// verileri liste haline getiriyorum
            return Ok(get);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var getId = await _context.Competitors.FindAsync(id);// id yakalıyoruz


            if (getId is null)
                return BadRequest();

            return Ok(getId);



        }
        [HttpGet("{categoryid}")]
        public async Task<IActionResult> CategoryIdGet(int categoryid)
        {
            var get = await _context.Competitors
                                    .Where(c=>c.CategoryId == categoryid)
                                    .ToListAsync();//categoriye ait olan id  where ile yakanarak bulunan veriler liste haline getirilir

            if (get is null)
                return BadRequest();

            return Ok (get);

        }

        [HttpPost]
        public async Task<IActionResult> PostCompetitor([FromBody] CompetitorEntity competitor) 
        {
            if (ModelState.IsValid)
            {
                await _context.Competitors.AddAsync(competitor);
                await _context.SaveChangesAsync();//veriler database aktarılır
                return CreatedAtAction(nameof(GetById), new { id = competitor.Id }, competitor);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateCompetitor(int id, [FromBody] CompetitorEntity update)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var getId = await _context.Competitors.FindAsync(id);
            if (getId is null)
                return BadRequest();

            getId.ModifiedDate = DateTime.Now;//değişiklik tarihi şuan olarak tanımlanır
            getId.FirstName=update.FirstName;
            getId.LastName=update.LastName;
            getId.Age=update.Age;   
            
            await _context.SaveChangesAsync();
            return Ok(getId);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var control = await _context.Competitors.FindAsync(id);
            if(control is null ) 
                return BadRequest($"{id} id numaralı veri bulunamadı");
            control.IsDelete = true;//soft delete yapılıyor
            await _context.SaveChangesAsync();
            return NoContent();


        }








    }
}
