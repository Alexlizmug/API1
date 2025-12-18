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
    public class EskaeraController : ControllerBase
    {
        private readonly EskaerakRepository _repo;

        public EskaeraController(EskaerakRepository repo)
        {
            _repo = repo;
        }

        // GET api/Eskaera
        [HttpGet]
        public ActionResult<IEnumerable<EskaeraDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new EskaeraDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    Data = e.Data,
                    Prezioa = e.Prezioa,
                    MahaiakId = e.MahaiakId,
                    Egoera = e.Egoera
                })
                .ToList();

            return Ok(list);
        }

        // GET api/Eskaera/5
        [HttpGet("{id:int}")]
        public ActionResult<EskaeraDto> GetById(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            var dto = new EskaeraDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Data = entity.Data,
                Prezioa = entity.Prezioa,
                MahaiakId = entity.MahaiakId,
                Egoera = entity.Egoera
            };

            return Ok(dto);
        }

        // POST api/Eskaera
        [HttpPost]
        public ActionResult<EskaeraDto> Create([FromBody] EskaeraSortuDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Eskaerak
            {
                Izena = dto.Izena,
                Data = dto.Data,
                Prezioa = dto.Prezioa,
                MahaiakId = dto.MahaiakId,
                Egoera = dto.Egoera
            };

            _repo.Add(entity);

            var result = new EskaeraDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Data = entity.Data,
                Prezioa = entity.Prezioa,
                MahaiakId = entity.MahaiakId,
                Egoera = entity.Egoera
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        // PUT api/Eskaera/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] EskaeraUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            entity.Izena = dto.Izena;
            entity.Data = dto.Data;
            entity.Prezioa = dto.Prezioa;
            entity.MahaiakId = dto.MahaiakId;
            entity.Egoera = dto.Egoera;

            _repo.Update(entity);

            return NoContent();
        }

        // DELETE api/Eskaera/5
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
