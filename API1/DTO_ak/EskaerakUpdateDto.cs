using System;

namespace API1.DTO_ak
{
    public class EskaerakUpdateDto
    {
        public string? Izena { get; set; }
        public float? Prezioa { get; set; }
        public DateTime? Data { get; set; }
        public int? Egoera { get; set; }
        public int? ZerbitzuaId { get; set; }
        public int? ProduktuaId { get; set; }
    }
}
