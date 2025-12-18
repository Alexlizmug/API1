using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API1.DTO_ak;
using API1.Modeloak;
using API1.Repositorioak;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZerbitzuaController : ControllerBase
    {
        private readonly ZerbitzuaRepository _repo;

        public ZerbitzuaController(ZerbitzuaRepository repo)
        {
            _repo = repo;
        }

        // GET api/Zerbitzua
        [HttpGet]
        public ActionResult<IEnumerable<ZerbitzuaDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(z => new ZerbitzuaDto
                {
                    Id = z.Id,
                    Izena = z.Izena,
                    Prezioa = z.Prezioa
                })
                .ToList();

            return Ok(list);
        }

        // GET api/Zerbitzua/5
        [HttpGet("{id:int}")]
        public ActionResult<ZerbitzuaDto> GetById(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            var dto = new ZerbitzuaDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Prezioa = entity.Prezioa
            };

            return Ok(dto);
        }

        // POST api/Zerbitzua
        [HttpPost]
        public ActionResult<ZerbitzuaDto> Create([FromBody] ZerbitzuaSortuDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Zerbitzua
            {
                Izena = dto.Izena,
                Prezioa = dto.Prezioa
            };

            _repo.Add(entity);

            var result = new ZerbitzuaDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Prezioa = entity.Prezioa
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        // PUT api/Zerbitzua/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ZerbitzuaUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            entity.Izena = dto.Izena;
            entity.Prezioa = dto.Prezioa;

            _repo.Update(entity);

            return NoContent();
        }

        // DELETE api/Zerbitzua/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            _repo.Delete(entity);

            return NoContent();
        }
    }
}
