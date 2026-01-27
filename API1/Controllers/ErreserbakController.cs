using API1.DTO_ak;
using API1.Modeloak;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErreserbakController : ControllerBase
    {
        private readonly NHSession _session;

        public ErreserbakController(NHSession session)
        {
            _session = session;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ErreserbakDto>> Get(DateTime data, bool mota)
        {
            var erreserbak = _session.Query<Erreserbak>()
                .Where(e => e.Data.Date == data.Date && e.Mota == mota)
                .ToList();

            var dto = erreserbak.Select(e => new ErreserbakDto
            {
                Id = e.Id,
                Data = e.Data,
                Mota = e.Mota,
                ErabiltzaileakId = e.ErabiltzaileakId,
                MahaiakId = e.MahaiakId
            });

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ErreserbakSortuDto dto)
        {
            using var tx = _session.BeginTransaction();

            var entity = new Erreserbak
            {
                Data = dto.Data,
                Mota = dto.Mota,
                ErabiltzaileakId = dto.ErabiltzaileakId,
                MahaiakId = dto.MahaiakId
            };

            _session.Save(entity);
            tx.Commit();

            return Ok(new { entity.Id });
        }

        [HttpPut("mahaia/{mahaiaId}")]
        public IActionResult UpdateByMahai(
            int mahaiaId,
            [FromQuery] DateTime data,
            [FromQuery] bool mota,
            [FromBody] ErreserbakSortuDto dto)

        {
            using var tx = _session.BeginTransaction();

            var entity = _session.Query<Erreserbak>()
                .FirstOrDefault(e =>
                    e.MahaiakId == mahaiaId &&
                    e.Data.Date == data.Date &&
                    e.Mota == mota);

            if (entity == null)
                return NotFound("Ez da erreserbarik aurkitu.");

            entity.Data = dto.Data;
            entity.Mota = dto.Mota;
            entity.ErabiltzaileakId = dto.ErabiltzaileakId;
            entity.MahaiakId = dto.MahaiakId;

            _session.Update(entity);
            tx.Commit();

            return Ok();
        }

        [HttpDelete("mahaia/{mahaiaId}")]
        public IActionResult DeleteByMahai(int mahaiaId, [FromQuery] DateTime data, [FromQuery] bool mota)
        {
            using var tx = _session.BeginTransaction();

            var entity = _session.Query<Erreserbak>()
                .FirstOrDefault(e =>
                    e.MahaiakId == mahaiaId &&
                    e.Data.Date == data.Date &&
                    e.Mota == mota);

            if (entity == null)
                return NotFound("Ez da erreserbarik aurkitu.");

            _session.Delete(entity);
            tx.Commit();

            return Ok("Erreserba ezabatuta.");
        }
    }
}
