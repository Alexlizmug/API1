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
    public class ErabiltzaileakController : ControllerBase
    {
        private readonly ErabiltzaileakRepository _repo;

        public ErabiltzaileakController(ErabiltzaileakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ErabiltzaileakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new ErabiltzaileakDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    Email = e.Email,
                    Pasahitza = e.Pasahitza,
                    Telefonoa = e.Telefonoa,
                    Abizena = e.Abizena
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ErabiltzaileakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new ErabiltzaileakDto
            {
                Id = e.Id,
                    Izena = e.Izena,
                    Email = e.Email,
                    Pasahitza = e.Pasahitza,
                    Telefonoa = e.Telefonoa,
                    Abizena = e.Abizena
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<ErabiltzaileakDto> Create([FromBody] ErabiltzaileakSortuDto dto)
        {
            var entity = new Erabiltzaileak
            {
                Izena = dto.Izena,
                Email = dto.Email,
                Pasahitza = dto.Pasahitza,
                Telefonoa = dto.Telefonoa,
                Abizena = dto.Abizena
            };

            _repo.Add(entity);

            var result = new ErabiltzaileakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena,
                    Email = entity.Email,
                    Pasahitza = entity.Pasahitza,
                    Telefonoa = entity.Telefonoa,
                    Abizena = entity.Abizena
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ErabiltzaileakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (!string.IsNullOrEmpty(dto.Email)) entity.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Pasahitza)) entity.Pasahitza = dto.Pasahitza;
            if (!string.IsNullOrEmpty(dto.Telefonoa)) entity.Telefonoa = dto.Telefonoa;
            if (!string.IsNullOrEmpty(dto.Abizena)) entity.Abizena = dto.Abizena;

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
