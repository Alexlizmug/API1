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
    public class ProduktuenEskaerakController : ControllerBase
    {
        private readonly ProduktuenEskaerakRepository _repo;

        public ProduktuenEskaerakController(ProduktuenEskaerakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProduktuenEskaerakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new ProduktuenEskaerakDto
                {
                    Id = e.Id,
                    Kantitatea = e.Kantitatea,
                    KantMax = e.KantMax,
                    KantMin = e.KantMin,
                    ProduktuakId = e.ProduktuakId
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProduktuenEskaerakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new ProduktuenEskaerakDto
            {
                Id = e.Id,
                    Kantitatea = e.Kantitatea,
                    KantMax = e.KantMax,
                    KantMin = e.KantMin,
                    ProduktuakId = e.ProduktuakId
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<ProduktuenEskaerakDto> Create([FromBody] ProduktuenEskaerakSortuDto dto)
        {
            var entity = new ProduktuenEskaerak
            {
                Kantitatea = dto.Kantitatea,
                KantMax = dto.KantMax,
                KantMin = dto.KantMin,
                ProduktuakId = dto.ProduktuakId
            };

            _repo.Add(entity);

            var result = new ProduktuenEskaerakDto
            {
                Id = entity.Id,
                    Kantitatea = entity.Kantitatea,
                    KantMax = entity.KantMax,
                    KantMin = entity.KantMin,
                    ProduktuakId = entity.ProduktuakId
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ProduktuenEskaerakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (dto.Kantitatea.HasValue) entity.Kantitatea = dto.Kantitatea;
            if (dto.KantMax.HasValue) entity.KantMax = dto.KantMax;
            if (dto.KantMin.HasValue) entity.KantMin = dto.KantMin;
            if (dto.ProduktuakId.HasValue) entity.ProduktuakId = dto.ProduktuakId.Value;

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
