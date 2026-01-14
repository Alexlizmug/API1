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
    public class PlaterakController : ControllerBase
    {
        private readonly PlaterakRepository _repo;

        public PlaterakController(PlaterakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlaterakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new PlaterakDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    Mota = e.Mota,
                    Perezioa = e.Perezioa,
                    PlateraMotakId = e.PlateraMotakId
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PlaterakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new PlaterakDto
            {
                Id = e.Id,
                    Izena = e.Izena,
                    Mota = e.Mota,
                    Perezioa = e.Perezioa,
                    PlateraMotakId = e.PlateraMotakId
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<PlaterakDto> Create([FromBody] PlaterakSortuDto dto)
        {
            var entity = new Platerak
            {
                Izena = dto.Izena,
                Mota = dto.Mota,
                Perezioa = dto.Perezioa,
                PlateraMotakId = dto.PlateraMotakId
            };

            _repo.Add(entity);

            var result = new PlaterakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena,
                    Mota = entity.Mota,
                    Perezioa = entity.Perezioa,
                    PlateraMotakId = entity.PlateraMotakId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] PlaterakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (!string.IsNullOrEmpty(dto.Mota)) entity.Mota = dto.Mota;
            if (dto.Perezioa.HasValue) entity.Perezioa = dto.Perezioa;
            if (dto.PlateraMotakId.HasValue) entity.PlateraMotakId = dto.PlateraMotakId.Value;

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
