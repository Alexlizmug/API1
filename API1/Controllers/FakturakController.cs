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
        private readonly ZerbitzuaRepository _zerbitzuaRepo;
        private readonly FakturakRepository _fakturakRepo;
        private readonly IWebHostEnvironment _env;

        public FakturakController(
            ZerbitzuaRepository zerbitzuaRepo,
            FakturakRepository fakturakRepo,
            IWebHostEnvironment env)
        {
            _zerbitzuaRepo = zerbitzuaRepo;
            _fakturakRepo = fakturakRepo;
            _env = env;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FakturaDto>> GetAll()
        {
            var fakturak = _fakturakRepo.GetAll()
                .Select(f => new FakturaDto
                {
                    Id = f.Id,
                    ZerbitzuaId = f.ZerbitzuaId,
                    PrezioTotala = f.PrezioTotala,
                    Sortuta = f.Sortuta,
                    Path = f.Path, 
                }).ToList();

            return Ok(fakturak);
        }

        [HttpPost("from-zerbitzua/{zerbitzuaId}")]
        public ActionResult<FakturaDto> CreateFromZerbitzua(int zerbitzuaId)
        {
            var zerbitzua = _zerbitzuaRepo.GetById(zerbitzuaId);
            if (zerbitzua == null) return NotFound("Zerbitzua ez da existitzen");

            var existing = _fakturakRepo.GetByZerbitzuaId(zerbitzuaId);
            if (existing != null)
                return BadRequest("Faktura dagoeneko existitzen da zerbitzu honentzat");

            var faktura = new Faktura
            {
                ZerbitzuaId = zerbitzua.Id,
                PrezioTotala = zerbitzua.PrezioTotala,
                Sortuta = false,
                Path = null
            };

            _fakturakRepo.Insert(faktura);

            return Ok(new FakturaDto
            {
                Id = faktura.Id,
                ZerbitzuaId = faktura.ZerbitzuaId,
                PrezioTotala = faktura.PrezioTotala,
                Sortuta = faktura.Sortuta,
                Path = faktura.Path
            });
        }

        [HttpGet("{id}/pdf")]
        public IActionResult DownloadPdf(int id)
        {
            var faktura = _fakturakRepo.GetById(id);
            if (faktura == null) return NotFound();

            var zerbitzua = _zerbitzuaRepo.GetById(faktura.ZerbitzuaId);
            if (zerbitzua == null) return NotFound();

            var bytes = PdfHelper.GenerateFakturaPdf(faktura, zerbitzua);

            var fileName = $"Faktura_{faktura.Id}.pdf";
            return File(bytes, "application/pdf", fileName);
        }


    }
}
