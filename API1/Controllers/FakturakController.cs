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
    public class FakturakController : ControllerBase
    {
        private readonly FakturakRepository _repo;

        public FakturakController(FakturakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FakturakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new FakturakDto
                {
                    Id = e.Id,
                    PrezioTotala = e.PrezioTotala,
                    Sortuta = e.Sortuta,
                    Path = e.Path
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<FakturakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new FakturakDto
            {
                Id = e.Id,
                    PrezioTotala = e.PrezioTotala,
                    Sortuta = e.Sortuta,
                    Path = e.Path
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<FakturakDto> Create([FromBody] FakturakSortuDto dto)
        {
            var entity = new Fakturak
            {
                PrezioTotala = dto.PrezioTotala,
                Sortuta = dto.Sortuta,
                Path = dto.Path
            };

            _repo.Add(entity);

            var result = new FakturakDto
            {
                Id = entity.Id,
                    PrezioTotala = entity.PrezioTotala,
                    Sortuta = entity.Sortuta,
                    Path = entity.Path
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] FakturakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (dto.PrezioTotala.HasValue) entity.PrezioTotala = dto.PrezioTotala;
            if (dto.Sortuta.HasValue) entity.Sortuta = dto.Sortuta;
            if (!string.IsNullOrEmpty(dto.Path)) entity.Path = dto.Path;

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
