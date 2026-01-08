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
    public class PlateraMotakController : ControllerBase
    {
        private readonly PlateraMotakRepository _repo;

        public PlateraMotakController(PlateraMotakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlateraMotakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new PlateraMotakDto
                {
                    Id = e.Id,
                    Izena = e.Izena
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PlateraMotakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new PlateraMotakDto
            {
                Id = e.Id,
                    Izena = e.Izena
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<PlateraMotakDto> Create([FromBody] PlateraMotakSortuDto dto)
        {
            var entity = new PlateraMotak
            {
                Izena = dto.Izena
            };

            _repo.Add(entity);

            var result = new PlateraMotakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] PlateraMotakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;

            _repo.Update(entity);
            return NoContent();
        }

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
