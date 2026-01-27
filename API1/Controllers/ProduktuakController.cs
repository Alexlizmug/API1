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
    public class ProduktuakController : ControllerBase
    {
        private readonly ProduktuakRepository _repo;

        public ProduktuakController(ProduktuakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProduktuakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(p => new ProduktuakDto
                {
                    Id = p.Id,
                    Izena = p.Izena,
                    Prezioa = p.Prezioa,
                    Stock = p.Stock,
                    stock_min = p.stock_min,
                    stock_max = p.stock_max,
                    Irudia = p.Irudia,
                    ProduktuenMotakId = p.ProduktuenMotakId
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProduktuakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new ProduktuakDto
            {
                Id = e.Id,
                    Izena = e.Izena,
                    Prezioa = e.Prezioa,
                    Stock = e.Stock,
                    Irudia = e.Irudia,
                    ProduktuenMotakId = e.ProduktuenMotakId
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<ProduktuakDto> Create([FromBody] ProduktuakSortuDto dto)
        {
            var entity = new Produktuak
            {
                Izena = dto.Izena,
                Prezioa = dto.Prezioa,
                Stock = dto.Stock,
                Irudia = dto.Irudia,
                ProduktuenMotakId = dto.ProduktuenMotakId
            };

            _repo.Add(entity);

            var result = new ProduktuakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena,
                    Prezioa = entity.Prezioa,
                    Stock = entity.Stock,
                    Irudia = entity.Irudia,
                    ProduktuenMotakId = entity.ProduktuenMotakId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ProduktuakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;
            if (dto.Prezioa.HasValue) entity.Prezioa = dto.Prezioa;
            if (dto.Stock.HasValue) entity.Stock = dto.Stock;
            if (!string.IsNullOrEmpty(dto.Irudia)) entity.Irudia = dto.Irudia;
            if (dto.ProduktuenMotakId.HasValue) entity.ProduktuenMotakId = dto.ProduktuenMotakId.Value;

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
