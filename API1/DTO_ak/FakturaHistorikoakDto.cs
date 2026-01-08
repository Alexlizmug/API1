using System;

namespace API1.DTO_ak
{
    public class FakturaHistorikoakDto
    {
        public int Id { get; set; }
        public string? Izena { get; set; }
        public float? PrezioTotala { get; set; }
        public DateTime? Data { get; set; }
        public int EskaerenKutxaId { get; set; }
    }
}
