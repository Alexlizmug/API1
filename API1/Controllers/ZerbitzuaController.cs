using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API1.DTO_ak;
using API1.Modeloak;
using NHibernate.Linq;

using NHSession = NHibernate.ISession;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZerbitzuaController : ControllerBase
    {
        private readonly NHSession _session;

        public ZerbitzuaController(NHSession session)
        {
            _session = session;
        }

        [HttpGet("mahaia/{mahaiaId:int}")]
        public ActionResult<IEnumerable<ZerbitzuaDto>> GetByMahai(int mahaiaId)
        {
            var zerbitzuak = _session.Query<Zerbitzua>()
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
                Ordainduta = z.Ordainduta,
                Eskaerak = z.Eskaerak
                    .Select(e => new EskaerakDto
                    {
                        Id = e.Id,
                        ProduktuaId = e.ProduktuaId,
                        Izena = e.Izena,
                        Prezioa = e.Prezioa,
                        Data = e.Data,
                        Egoera = e.Egoera
                    })
                    .ToList()
            }).ToList();

            return Ok(dtoList);
        }

        [HttpPost]
        public ActionResult<ZerbitzuaDto> Create([FromBody] ZerbitzuaSortuDto dto)
        {
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
                        Zerbitzua = entity
                    };
                    var produktua = _session.Query<Produktuak>()
                                            .FirstOrDefault(p => p.Id == e.ProduktuaId);

                    if (produktua == null)
                        return BadRequest($"Produktua ez da existitzen: {e.ProduktuaId}");

                    var stock = produktua.Stock ?? 0;
                    if (stock <= 0)
                        return BadRequest($"Stock gabe: {produktua.Izena}");

                    produktua.Stock = stock - 1;
                    _session.Update(produktua);
                    _session.Save(eskaera);
                }

                tx.Commit();

                var resultDto = new ZerbitzuaDto
                {
                    Id = entity.Id,
                    PrezioTotala = entity.PrezioTotala,
                    Data = entity.Data,
                    ErreserbaId = entity.ErreserbaId,
                    MahaiakId = entity.MahaiakId
                };

                return CreatedAtAction(nameof(GetByMahai), new { mahaiaId = entity.MahaiakId }, resultDto);
            }
        }

        [HttpPost("{id:int}/ordaindu")]
        public IActionResult Ordaindu(int id)
        {
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    var zerbitzua = _session.Query<Zerbitzua>().FirstOrDefault(z => z.Id == id);
                    if (zerbitzua == null)
                        return NotFound("Zerbitzua ez da aurkitu.");

                    var eskaerak = _session.Query<Eskaerak>()
                                           .Where(e => e.Zerbitzua.Id == id)
                                           .ToList();

                    var total = eskaerak.Sum(e => (decimal)(e.Prezioa ?? 0f));
                    zerbitzua.PrezioTotala = (float)total;
                    zerbitzua.Ordainduta = true;
                    _session.Update(zerbitzua);

                    foreach (var eskaera in eskaerak)
                    {
                        eskaera.Egoera = 1;
                        _session.Update(eskaera);
                    }

                    var fakturaExist = _session.Query<Faktura>().FirstOrDefault(f => f.ZerbitzuaId == id);
                    Faktura faktura;
                    if (fakturaExist == null)
                    {
                        faktura = new Faktura
                        {
                            ZerbitzuaId = zerbitzua.Id,
                            PrezioTotala = zerbitzua.PrezioTotala,
                        };
                        _session.Save(faktura);
                    }
                    else
                    {
                        faktura = fakturaExist;
                    }

                    tx.Commit();

                    return Ok(new
                    {
                        FakturaId = faktura.Id,
                        ZerbitzuaId = id,
                        PrezioTotala = zerbitzua.PrezioTotala,
                        EskaeraKopurua = eskaerak.Count
                    });
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return StatusCode(500, "Errorea prozesuan: " + ex.Message);
                }
            }
        }


    }
}
