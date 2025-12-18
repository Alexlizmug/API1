using System;

namespace API1.DTO_ak
{
    public class EskaeraDto
    {
        public int Id { get; set; }
        public string Izena { get; set; }
        public DateTime? Data { get; set; }
        public decimal Prezioa { get; set; }
        public int? MahaiakId { get; set; }
        public string Egoera { get; set; }
    }
}

