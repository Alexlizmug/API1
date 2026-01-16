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
        public ActionResult<IEnumerable<FakturakDto>> GetAll()
        {
            var fakturak = _fakturakRepo.GetAll()
                .Select(f => new FakturakDto
                {
                    Id = f.Id,
                    ZerbitzuaId = f.ZerbitzuaId,
                    PrezioTotala = f.PrezioTotala,
                    Sortuta = f.Sortuta,
                    Path = f.Path
                }).ToList();

            return Ok(fakturak);
        }

        [HttpPost("from-zerbitzua/{zerbitzuaId}")]
        public ActionResult<FakturakDto> CreateFromZerbitzua(int zerbitzuaId)
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

            return Ok(new FakturakDto
            {
                Id = faktura.Id,
                ZerbitzuaId = faktura.ZerbitzuaId,
                PrezioTotala = faktura.PrezioTotala,
                Sortuta = faktura.Sortuta,
                Path = faktura.Path
            });
        }

        [HttpGet("{id}/pdf")]
        public IActionResult PdfDeskargatu(int id)
        {
            var faktura = _fakturakRepo.GetById(id);
            if (faktura == null) return NotFound();

            if (faktura.Sortuta && !string.IsNullOrEmpty(faktura.Path))
            {
                var fullPath = Path.Combine(_env.ContentRootPath, faktura.Path);
                if (!System.IO.File.Exists(fullPath)) return NotFound("PDFa ez da aurkitu");

                var bytesExisting = System.IO.File.ReadAllBytes(fullPath);
                return File(bytesExisting, "application/pdf", Path.GetFileName(fullPath));
            }

            var zerbitzua = _zerbitzuaRepo.GetById(faktura.ZerbitzuaId);
            if (zerbitzua == null) return NotFound("Zerbitzua ez da aurkitu");

            var bytes = PdfHelper.GenerateFakturaPdf(faktura, zerbitzua);

            var fakturakFolder = Path.Combine(_env.WebRootPath, "fakturak");
            Directory.CreateDirectory(fakturakFolder);

            var fileName = $"Faktura_{faktura.Id}.pdf";
            var fullFilePath = Path.Combine(fakturakFolder, fileName);
            System.IO.File.WriteAllBytes(fullFilePath, bytes);

            faktura.Sortuta = true;
            faktura.Path = Path.Combine("fakturak", fileName);
            _fakturakRepo.Update(faktura);

            return File(bytes, "application/pdf");

        }



    }
}
