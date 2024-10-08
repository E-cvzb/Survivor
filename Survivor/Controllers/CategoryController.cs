using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Context;
using Survivor.Entities;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SurvivorDbContext _context;//database ile bağlantı sağlıyoruz
        public CategoryController(SurvivorDbContext context )
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> CategoryGet()
        {
            var get = await _context.Categorys.ToListAsync();//verileri listeye çeviriyoruz
            return Ok(get);
        }

        [HttpGet("{id}")]
        public async Task< IActionResult> GetById(int id)
        {
            var control = await _context.Categorys.FindAsync(id);// id yi yakalamk için cotrol tanımlıyoruz ve if ile kotrol ediyoruz
            if (control is null)
                return NotFound();


            return Ok(control);
        }


        [HttpPost]
        public async Task <IActionResult> PostCategory([FromBody] CategoryEntity category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categorys.AddAsync(category);
                await _context.SaveChangesAsync();  //verileri kyıt ederk datebase akatarıyoruz
                return CreatedAtAction(nameof(GetById), new {id=category.Id},category);

            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryEntity category)
        {
            if (!ModelState.IsValid)//istenen durum salanıp sağlanmıyor mu onu kotrol ediyoruz
                return NotFound();
            var update = await _context.Categorys.FindAsync(id);
            if (update is null)
                return BadRequest($"{id} nnumaralı kayıt bulunamadı");

            update.Name= category.Name;
            update.ModifiedDate= DateTime.Now;
            
            await _context.SaveChangesAsync();  
            return Ok(update);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            var control= await _context.Categorys.FindAsync(id);
            if (control is null) 
                return NotFound();

            control.IsDelete = true;//soft delete yapıyoruz
            await _context.SaveChangesAsync();

            return NoContent();

        }
     }
}
