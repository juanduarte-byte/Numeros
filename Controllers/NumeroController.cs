using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParImparAPI.Domain.Data;
using ParImparAPI.Domain.Entities;

namespace ParImparAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NumeroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NumeroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/Numero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Numero>>> Get()
        {
            return await _context.Numeros.ToListAsync();
        }

        // GET: api/v1/Numero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Numero>> Get(int id)
        {
            var numero = await _context.Numeros.FindAsync(id);

            if (numero == null)
                return NotFound();

            return numero;
        }

        // POST: api/v1/Numero
        [HttpPost]
        public async Task<ActionResult<Numero>> Post(Numero numero)
        {
            // Calcular la paridad automáticamente
            numero.Paridad = (numero.Value % 2 == 0) ? "Par" : "Impar";

            // Agregar el número a la base de datos
            _context.Numeros.Add(numero);
            await _context.SaveChangesAsync();

            // Devolver la respuesta con el id generado
            return CreatedAtAction(nameof(Get), new { id = numero.Id }, numero);
        }

        // PUT: api/v1/Numero/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Numero numero)
        {
            if (id != numero.Id)
                return BadRequest();

            // Calcular la paridad nuevamente
            numero.Paridad = (numero.Value % 2 == 0) ? "Par" : "Impar";

            _context.Entry(numero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Numeros.Any(n => n.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/v1/Numero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var numero = await _context.Numeros.FindAsync(id);

            if (numero == null)
                return NotFound();

            _context.Numeros.Remove(numero);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
