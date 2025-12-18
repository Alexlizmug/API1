using System;

namespace API1.Modeloak
{
    public class Erreserbak
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual string Mota { get; set; }
        public virtual int? ErabiltzaileakId { get; set; }
        public virtual int? MahaiaId { get; set; }
    }
}
