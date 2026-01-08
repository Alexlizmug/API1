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
    public class ProduktuenMotakController : ControllerBase
    {
        private readonly ProduktuenMotakRepository _repo;

        public ProduktuenMotakController(ProduktuenMotakRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProduktuenMotakDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(e => new ProduktuenMotakDto
                {
                    Id = e.Id,
                    Izena = e.Izena
                });

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProduktuenMotakDto> GetById(int id)
        {
            var e = _repo.GetById(id);
            if (e == null) return NotFound();

            var dto = new ProduktuenMotakDto
            {
                Id = e.Id,
                    Izena = e.Izena
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<ProduktuenMotakDto> Create([FromBody] ProduktuenMotakSortuDto dto)
        {
            var entity = new ProduktuenMotak
            {
                Izena = dto.Izena
            };

            _repo.Add(entity);

            var result = new ProduktuenMotakDto
            {
                Id = entity.Id,
                    Izena = entity.Izena
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ProduktuenMotakUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.Izena)) entity.Izena = dto.Izena;

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
