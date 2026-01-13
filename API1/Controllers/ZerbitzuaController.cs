using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API1.DTO_ak;
using API1.Modeloak;
using API1.Repositorioak;
<<<<<<< Updated upstream
=======
using NHibernate.Linq;

using NHSession = NHibernate.ISession;
>>>>>>> Stashed changes

namespace API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZerbitzuaController : ControllerBase
    {
        private readonly ZerbitzuaRepository _repo;

        public ZerbitzuaController(ZerbitzuaRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ZerbitzuaDto>> GetAll()
        {
            var list = _repo.GetAll()
                .Select(z => new ZerbitzuaDto
                {
                    Id = z.Id,
                    Izena = z.Izena,
                    Prezioa = z.Prezioa
                })
                .ToList();

            return Ok(list);
        }

        
        [HttpGet("{id:int}")]
        public ActionResult<ZerbitzuaDto> GetById(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            var dto = new ZerbitzuaDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Prezioa = entity.Prezioa
            };

            return Ok(dto);
        }

        [HttpGet("mahaia/{mahaiaId:int}")]
        public ActionResult<IEnumerable<ZerbitzuaDto>> GetByMahai(int mahaiaId)
        {
            var zerbitzuak = _repo.GetAll()
                .Where(z => z.MahaiakId == mahaiaId)
                .OrderByDescending(z => z.Data)
                .ToList();
            var dtoList = zerbitzuak.Select(z => new ZerbitzuaDto
            {
                Id = z.Id,
                PrezioTotala = z.PrezioTotala,
                Data = z.Data,
                ErreserbaId = z.ErreserbaId,
                MahaiakId = z.MahaiakId,
                Eskaerak = _session.Query<Eskaerak>()
                    .Where(e => e.ZerbitzuaId == z.Id)
                    .Select(e => new EskaerakDto
                    {
                        Id = e.Id,
                        ProduktuaId = e.ProduktuaId,
                        Izena = e.Izena,
                        Prezioa = e.Prezioa,
                        Data = e.Data,
                        Egoera = e.Egoera,
                    })
                    .ToList()
            }).ToList();
            return Ok(dtoList);
        }

        [HttpPost]
        public ActionResult<ZerbitzuaDto> Create([FromBody] ZerbitzuaSortuDto dto)
        {
<<<<<<< Updated upstream
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Zerbitzua
            {
                Izena = dto.Izena,
                Prezioa = dto.Prezioa
            };

            _repo.Add(entity);

            var result = new ZerbitzuaDto
            {
                Id = entity.Id,
                Izena = entity.Izena,
                Prezioa = entity.Prezioa
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
=======
            using (var tx = _session.BeginTransaction())
            {
                var entity = new Zerbitzua
                {
                    PrezioTotala = dto.PrezioTotala,
                    Data = dto.Data,
                    ErreserbaId = dto.ErreserbaId,
                    MahaiakId = dto.MahaiakId
                };
                _session.Save(entity);

                foreach (var e in dto.Eskaerak)
                {
                    var eskaera = new Eskaerak
                    {
                        ProduktuaId = e.ProduktuaId,
                        Izena = e.Izena,
                        Prezioa = e.Prezioa,
                        Data = e.Data,
                        Egoera = e.Egoera,
                        ZerbitzuaId = entity.Id,
                    };
                    Console.WriteLine($"API recibe ProduktuaId = {e.ProduktuaId}");
                    _session.Save(eskaera);
                }
                tx.Commit();
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new ZerbitzuaDto
                {
                    Id = entity.Id,
                    PrezioTotala = entity.PrezioTotala,
                    Data = entity.Data,
                    ErreserbaId = entity.ErreserbaId,
                    MahaiakId = entity.MahaiakId
                });
            }
>>>>>>> Stashed changes
        }

        
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ZerbitzuaUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _repo.GetById(id);
            if (entity == null) return NotFound();

            entity.Izena = dto.Izena;
            entity.Prezioa = dto.Prezioa;

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
<<<<<<< Updated upstream
=======

        
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

                foreach (var eskaera in eskaerak)
                {
                    eskaera.Egoera = 1;
                    _session.Update(eskaera);
                }

                tx.Commit();

                return Ok(new
                {
                    ZerbitzuaId = zerbitzua.Id,
                    PrezioTotala = zerbitzua.PrezioTotala,
                    EskaeraKopurua = eskaerak.Count
                });
            }
        }
>>>>>>> Stashed changes
    }
}
