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
    public class EskaerakController : ControllerBase
    {
        private readonly EskaerakRepository _repo;

        public EskaerakController(EskaerakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EskaerakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new EskaerakDto
                {
                    Id = e.Id,
                    Izena = e.Izena,
                    Prezioa = e.Prezioa,
                    Data = e.Data,
                    Egoera = e.Egoera,
                    ZerbitzuaId = e.ZerbitzuaId,
                    ProduktuaId = e.ProduktuaId
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EskaerakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new EskaerakDto
            {
                Id = e.Id,
                    Izena = e.Izena,
                    Prezioa = e.Prezioa,
                    Data = e.Data,
                    Egoera = e.Egoera,
                    ZerbitzuaId = e.ZerbitzuaId,
                    ProduktuaId = e.ProduktuaId
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<EskaerakDto> Create([FromBody] EskaerakSortuDto dto)
        {
            var entity = new Eskaerak
            {
                Izena = dto.Izena,
                Prezioa = dto.Prezioa,
                Data = dto.Data,
                Egoera = dto.Egoera,
                ZerbitzuaId = dto.ZerbitzuaId,
                ProduktuaId = dto.ProduktuaId
            };

            _repo.Add(entity);

            var result = new EskaerakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena,
                    Prezioa = entity.Prezioa,
                    Data = entity.Data,
                    Egoera = entity.Egoera,
                    ZerbitzuaId = entity.ZerbitzuaId,
                    ProduktuaId = entity.ProduktuaId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] EskaerakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (dto.Prezioa.HasValue) entity.Prezioa = dto.Prezioa;
            if (dto.Data.HasValue) entity.Data = dto.Data;
            if (dto.Egoera.HasValue) entity.Egoera = dto.Egoera;
            entity.ZerbitzuaId = dto.ZerbitzuaId;
            if (dto.ProduktuaId.HasValue) entity.ProduktuaId = dto.ProduktuaId.Value;

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
