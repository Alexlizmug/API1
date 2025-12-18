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
    public class ErreserbaController : ControllerBase
    {
        private readonly ErreserbakRepository _repo;

        public ErreserbaController(ErreserbakRepository repo)
        {
            _repo = repo;
        }

        // GET api/Erreserba
        [HttpGet]
        public ActionResult<IEnumerable<ErreserbaDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new ErreserbaDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    Data = e.Data,
                    Mota = e.Mota,
                    ErabiltzaileakId = e.ErabiltzaileakId,
                    MahaiaId = e.MahaiaId
                });

            return Ok(list);
        }

        // GET api/Erreserba/5
        [HttpGet("{id:int}")]
        public ActionResult<ErreserbaDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new ErreserbaDto
            {
                Id = e.Id,
                Izena = e.Izena,
                Data = e.Data,
                Mota = e.Mota,
                ErabiltzaileakId = e.ErabiltzaileakId,
                MahaiaId = e.MahaiaId
            };

            return Ok(dto);
        }

        // POST api/Erreserba
        [HttpPost]
        public ActionResult<ErreserbaDto> Create([FromBody] ErreserbaSortuDto dto)
        {
            var entity = new Erreserbak
            {
                Izena = dto.Izena,
                Data = dto.Data,
                Mota = dto.Mota,
                ErabiltzaileakId = dto.ErabiltzaileakId,
                MahaiaId = dto.MahaiaId
            };

            _repo.Add(entity);

            var result = new ErreserbaDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Data = entity.Data,
                Mota = entity.Mota,
                ErabiltzaileakId = entity.ErabiltzaileakId,
                MahaiaId = entity.MahaiaId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        // PUT api/Erreserba/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ErreserbaUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (dto.Data.HasValue) entity.Data = dto.Data;
            if (!string.IsNullOrEmpty(dto.Mota)) entity.Mota = dto.Mota;
            if (dto.ErabiltzaileakId.HasValue) entity.ErabiltzaileakId = dto.ErabiltzaileakId;
            if (dto.MahaiaId.HasValue) entity.MahaiaId = dto.MahaiaId;

            _repo.Update(entity);

            return NoContent();
        }

        // DELETE api/Erreserba/5
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
