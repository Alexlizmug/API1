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
    public class LangileaController : ControllerBase
    {
        private readonly LangileakRepository _repo;

        public LangileaController(LangileakRepository repo)
        {
            _repo = repo;
        }

        // GET api/Langilea
        [HttpGet]
        public ActionResult<IEnumerable<LangileaDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(l => new LangileaDto
                {
                    Id = l.Id,
                    Izena = l.Izena,
                    Abizena = l.Abizena,
                    Email = l.Email,
                    Telefonoa = l.Telefonoa,
                    Baimena = l.Baimena,
                    Erabiltzailea = l.Erabiltzailea
                });

            return Ok(list);
        }

        // GET api/Langilea/5
        [HttpGet("{id:int}")]
        public ActionResult<LangileaDto> GetById(int id)
        {
            var l = _repo.GetById(id);
            if (l == null) return NotFound();

            var dto = new LangileaDto
            {
                Id = l.Id,
                Izena = l.Izena,
                Abizena = l.Abizena,
                Email = l.Email,
                Telefonoa = l.Telefonoa,
                Baimena = l.Baimena,
                Erabiltzailea = l.Erabiltzailea
            };

            return Ok(dto);
        }

        // POST api/Langilea
        [HttpPost]
        public ActionResult<LangileaDto> Create([FromBody] LangileaSortuDto dto)
        {
            var entity = new Langileak
            {
                Izena = dto.Izena,
                Abizena = dto.Abizena,
                Email = dto.Email,
                Telefonoa = dto.Telefonoa,
                Baimena = dto.Baimena,
                Erabiltzailea = dto.Erabiltzailea,
                Pasahitza = dto.Pasahitza
            };

            _repo.Add(entity);

            var result = new LangileaDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Abizena = entity.Abizena,
                Email = entity.Email,
                Telefonoa = entity.Telefonoa,
                Baimena = entity.Baimena,
                Erabiltzailea = entity.Erabiltzailea
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        // PUT api/Langilea/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] LangileaUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (!string.IsNullOrEmpty(dto.Abizena)) entity.Abizena = dto.Abizena;
            if (!string.IsNullOrEmpty(dto.Email)) entity.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Telefonoa)) entity.Telefonoa = dto.Telefonoa;
            if (dto.Baimena.HasValue) entity.Baimena = dto.Baimena.Value;
            if (!string.IsNullOrEmpty(dto.Erabiltzailea)) entity.Erabiltzailea = dto.Erabiltzailea;
            if (!string.IsNullOrEmpty(dto.Pasahitza)) entity.Pasahitza = dto.Pasahitza;

            _repo.Update(entity);

            return NoContent();
        }

        // DELETE api/Langilea/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            _repo.Delete(entity);

            return NoContent();
        }

        // POST api/Langilea/login
        [HttpPost("login")]
        public ActionResult<LangileaDto> Login([FromBody] LoginDto dto)
        {
            var user = _repo.GetByCredentials(dto.Erabiltzailea, dto.Pasahitza);
            if (user == null)
                return Unauthorized("Erabiltzailea edo pasahitza okerra da");

            var result = new LangileaDto
            {
                Id = user.Id,
                Izena = user.Izena,
                Abizena = user.Abizena,
                Email = user.Email,
                Telefonoa = user.Telefonoa,
                Baimena = user.Baimena,
                Erabiltzailea = user.Erabiltzailea
            };

            return Ok(result);
        }
    }
}
