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
    public class ErreserbakController : ControllerBase
    {
        private readonly ErreserbakRepository _repo;

        public ErreserbakController(ErreserbakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ErreserbakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new ErreserbakDto
                {
                    Id = e.Id,
                    Data = e.Data,
                    Mota = e.Mota,
                    ErabiltzaileakId = e.ErabiltzaileakId,
                    MahaiakId = e.MahaiakId
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ErreserbakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new ErreserbakDto
            {
                Id = e.Id,
                    Data = e.Data,
                    Mota = e.Mota,
                    ErabiltzaileakId = e.ErabiltzaileakId,
                    MahaiakId = e.MahaiakId
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<ErreserbakDto> Create([FromBody] ErreserbakSortuDto dto)
        {
            var entity = new Erreserbak
            {
                Data = dto.Data,
                Mota = dto.Mota,
                ErabiltzaileakId = dto.ErabiltzaileakId,
                MahaiakId = dto.MahaiakId
            };

            _repo.Add(entity);

            var result = new ErreserbakDto
            {
                Id = entity.Id,
                    Data = entity.Data,
                    Mota = entity.Mota,
                    ErabiltzaileakId = entity.ErabiltzaileakId,
                    MahaiakId = entity.MahaiakId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ErreserbakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (dto.Data.HasValue) entity.Data = dto.Data;
            if (dto.Mota.HasValue) entity.Mota = dto.Mota;
            if (dto.ErabiltzaileakId.HasValue) entity.ErabiltzaileakId = dto.ErabiltzaileakId.Value;
            if (dto.MahaiakId.HasValue) entity.MahaiakId = dto.MahaiakId.Value;

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
