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
    public class MahaiakController : ControllerBase
    {
        private readonly MahaiakRepository _repo;

        public MahaiakController(MahaiakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MahaiakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new MahaiakDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    Egoera = e.Egoera
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<MahaiakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new MahaiakDto
            {
                Id = e.Id,
                    Izena = e.Izena,
                    Egoera = e.Egoera
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<MahaiakDto> Create([FromBody] MahaiakSortuDto dto)
        {
            var entity = new Mahaiak
            {
                Izena = dto.Izena,
                Egoera = dto.Egoera
            };

            _repo.Add(entity);

            var result = new MahaiakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena,
                    Egoera = entity.Egoera
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] MahaiakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (!string.IsNullOrEmpty(dto.Egoera)) entity.Egoera = dto.Egoera;

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
