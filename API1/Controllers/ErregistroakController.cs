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
    public class ErregistroakController : ControllerBase
    {
        private readonly ErregistroakRepository _repo;

        public ErregistroakController(ErregistroakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ErregistroakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new ErregistroakDto
                {
                    Id = e.Id,
                    Erabiltzailea = e.Erabiltzailea,
                    Pasahitza = e.Pasahitza,
                    Secret = e.Secret
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ErregistroakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new ErregistroakDto
            {
                Id = e.Id,
                    Erabiltzailea = e.Erabiltzailea,
                    Pasahitza = e.Pasahitza,
                    Secret = e.Secret
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<ErregistroakDto> Create([FromBody] ErregistroakSortuDto dto)
        {
            var entity = new Erregistroak
            {
                Erabiltzailea = dto.Erabiltzailea,
                Pasahitza = dto.Pasahitza,
                Secret = dto.Secret
            };

            _repo.Add(entity);

            var result = new ErregistroakDto
            {
                Id = entity.Id,
                    Erabiltzailea = entity.Erabiltzailea,
                    Pasahitza = entity.Pasahitza,
                    Secret = entity.Secret
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ErregistroakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Erabiltzailea)) entity.Erabiltzailea = dto.Erabiltzailea;
            if (!string.IsNullOrEmpty(dto.Pasahitza)) entity.Pasahitza = dto.Pasahitza;
            if (!string.IsNullOrEmpty(dto.Secret)) entity.Secret = dto.Secret;

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
