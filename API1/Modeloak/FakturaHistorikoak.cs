using System;

namespace API1.Modeloak
{
    public class FakturaHistorikoak
    {
        public virtual int Id { get; set; }
        public virtual string? Izena { get; set; }
        public virtual float? PrezioTotala { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual int EskaerenKutxaId { get; set; }
    }
}
