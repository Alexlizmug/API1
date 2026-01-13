using System;

namespace API1.DTO_ak
{
    public class EskaerakDto
    {
        public int Id { get; set; }
        public int ProduktuaId { get; set; }
        public string? Izena { get; set; }
        public float? Prezioa { get; set; }
        public DateTime? Data { get; set; }
        public int? Egoera { get; set; }
        public string EskaeraHeader => $"{Izena} - {Prezioa}€";
    }
}
