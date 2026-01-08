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
    public class LangileakController : ControllerBase
    {
        private readonly LangileakRepository _repo;

        public LangileakController(LangileakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LangileakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new LangileakDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    Abizena = e.Abizena,
                    Erabiltzailea = e.Erabiltzailea,
                    Pasahitza = e.Pasahitza,
                    Email = e.Email,
                    Telefonoa = e.Telefonoa,
                    Baimena = e.Baimena
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<LangileakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new LangileakDto
            {
                Id = e.Id,
                    Izena = e.Izena,
                    Abizena = e.Abizena,
                    Erabiltzailea = e.Erabiltzailea,
                    Pasahitza = e.Pasahitza,
                    Email = e.Email,
                    Telefonoa = e.Telefonoa,
                    Baimena = e.Baimena
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<LangileakDto> Create([FromBody] LangileakSortuDto dto)
        {
            var entity = new Langileak
            {
                Izena = dto.Izena,
                Abizena = dto.Abizena,
                Erabiltzailea = dto.Erabiltzailea,
                Pasahitza = dto.Pasahitza,
                Email = dto.Email,
                Telefonoa = dto.Telefonoa,
                Baimena = dto.Baimena
            };

            _repo.Add(entity);

            var result = new LangileakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena,
                    Abizena = entity.Abizena,
                    Erabiltzailea = entity.Erabiltzailea,
                    Pasahitza = entity.Pasahitza,
                    Email = entity.Email,
                    Telefonoa = entity.Telefonoa,
                    Baimena = entity.Baimena
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] LangileakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (!string.IsNullOrEmpty(dto.Abizena)) entity.Abizena = dto.Abizena;
            if (!string.IsNullOrEmpty(dto.Erabiltzailea)) entity.Erabiltzailea = dto.Erabiltzailea;
            if (!string.IsNullOrEmpty(dto.Pasahitza)) entity.Pasahitza = dto.Pasahitza;
            if (!string.IsNullOrEmpty(dto.Email)) entity.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Telefonoa)) entity.Telefonoa = dto.Telefonoa;
            if (dto.Baimena.HasValue) entity.Baimena = dto.Baimena;

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

        public record LoginRequest(string erabiltzailea, string pasahitza);

        [HttpPost("login")]
        public ActionResult<LangileakDto> Login([FromBody] LoginRequest req)
        {
            // Repo-ak GetAll() badauka, hortik bilatu dezakezu (momentuz nahikoa)
            var e = _repo.GetAll()
                .FirstOrDefault(x => x.Erabiltzailea == req.erabiltzailea
                                  && x.Pasahitza == req.pasahitza);

            if (e == null) return Unauthorized(); // 401

            // GOMENDIOA: ez itzuli pasahitza
            var dto = new LangileakDto
            {
                Id = e.Id,
                Izena = e.Izena,
                Abizena = e.Abizena,
                Erabiltzailea = e.Erabiltzailea,
                Pasahitza = null,          // edo kendu DTO-tik
                Email = e.Email,
                Telefonoa = e.Telefonoa,
                Baimena = e.Baimena
            };

            return Ok(dto);
        }
    }
}
