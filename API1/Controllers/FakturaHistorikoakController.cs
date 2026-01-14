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
    public class FakturaHistorikoakController : ControllerBase
    {
        private readonly FakturaHistorikoakRepository _repo;

        public FakturaHistorikoakController(FakturaHistorikoakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FakturaHistorikoakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new FakturaHistorikoakDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    PrezioTotala = e.PrezioTotala,
                    Data = e.Data,
                    EskaerenKutxaId = e.EskaerenKutxaId
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<FakturaHistorikoakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new FakturaHistorikoakDto
            {
                Id = e.Id,
                    Izena = e.Izena,
                    PrezioTotala = e.PrezioTotala,
                    Data = e.Data,
                    EskaerenKutxaId = e.EskaerenKutxaId
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<FakturaHistorikoakDto> Create([FromBody] FakturaHistorikoakSortuDto dto)
        {
            var entity = new FakturaHistorikoak
            {
                Izena = dto.Izena,
                PrezioTotala = dto.PrezioTotala,
                Data = dto.Data,
                EskaerenKutxaId = dto.EskaerenKutxaId
            };

            _repo.Add(entity);

            var result = new FakturaHistorikoakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena,
                    PrezioTotala = entity.PrezioTotala,
                    Data = entity.Data,
                    EskaerenKutxaId = entity.EskaerenKutxaId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] FakturaHistorikoakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (dto.PrezioTotala.HasValue) entity.PrezioTotala = dto.PrezioTotala;
            if (dto.Data.HasValue) entity.Data = dto.Data;
            if (dto.EskaerenKutxaId.HasValue) entity.EskaerenKutxaId = dto.EskaerenKutxaId.Value;

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
