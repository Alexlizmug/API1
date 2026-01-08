using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API1.DTO_ak;
using API1.Modeloak;
using API1.Repositorioak;
using NHibernate.Linq;

// ✅ Alias-a: hemen konpontzen da anbiguotasuna (ASP.NET ISession vs NHibernate ISession)
using NHSession = NHibernate.ISession;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZerbitzuaController : ControllerBase
    {
        private readonly ZerbitzuaRepository _repo;
        private readonly NHSession _session;

        public ZerbitzuaController(ZerbitzuaRepository repo, NHSession session)
        {
            _repo = repo;
            _session = session;
        }

        // GET api/Zerbitzua
        [HttpGet]
        public ActionResult<IEnumerable<ZerbitzuaDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(z => new ZerbitzuaDto
                {
                    Id = z.Id,
                    PrezioTotala = z.PrezioTotala,
                    Data = z.Data,
                    ErreserbaId = z.ErreserbaId,
                    MahaiakId = z.MahaiakId
                });

            return Ok(list);
        }

        // GET api/Zerbitzua/5
        [HttpGet("{id:int}")]
        public ActionResult<ZerbitzuaDto> GetById(int id)
        {
            var z = _repo.GetById(id);
            if (z == null) return NotFound();

            return Ok(new ZerbitzuaDto
            {
                Id = z.Id,
                PrezioTotala = z.PrezioTotala,
                Data = z.Data,
                ErreserbaId = z.ErreserbaId,
                MahaiakId = z.MahaiakId
            });
        }

        // POST api/Zerbitzua
        [HttpPost]
        public ActionResult<ZerbitzuaDto> Create([FromBody] ZerbitzuaSortuDto dto)
        {
            var entity = new Zerbitzua
            {
                PrezioTotala = dto.PrezioTotala,
                Data = dto.Data,
                ErreserbaId = dto.ErreserbaId,
                MahaiakId = dto.MahaiakId
            };

            _repo.Add(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new ZerbitzuaDto
            {
                Id = entity.Id,
                PrezioTotala = entity.PrezioTotala,
                Data = entity.Data,
                ErreserbaId = entity.ErreserbaId,
                MahaiakId = entity.MahaiakId
            });
        }

        // PUT api/Zerbitzua/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ZerbitzuaUpdateDto dto)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            if (dto.PrezioTotala.HasValue) entity.PrezioTotala = dto.PrezioTotala;
            if (dto.Data.HasValue) entity.Data = dto.Data;
            if (dto.ErreserbaId.HasValue) entity.ErreserbaId = dto.ErreserbaId;
            if (dto.MahaiakId.HasValue) entity.MahaiakId = dto.MahaiakId;

            _repo.Update(entity);
            return NoContent();
        }

        // DELETE api/Zerbitzua/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            _repo.Delete(entity);
            return NoContent();
        }

        // POST api/Zerbitzua/5/ordaindu
        [HttpPost("{id:int}/ordaindu")]
        public IActionResult Ordaindu(int id)
        {
            using (var tx = _session.BeginTransaction())
            {
                var zerbitzua = _session.Query<Zerbitzua>()
                    .FirstOrDefault(z => z.Id == id);

                if (zerbitzua == null)
                    return NotFound("Zerbitzua ez da aurkitu.");

                var eskaerak = _session.Query<Eskaerak>()
                    .Where(e => e.ZerbitzuaId == id)
                    .ToList();

                var total = eskaerak.Sum(e => (decimal)(e.Prezioa ?? 0f));
                zerbitzua.PrezioTotala = (float)total;

                _session.Update(zerbitzua);

                tx.Commit();

                return Ok(new
                {
                    ZerbitzuaId = zerbitzua.Id,
                    PrezioTotala = zerbitzua.PrezioTotala,
                    EskaeraKopurua = eskaerak.Count
                });
            }
        }
    }
}
