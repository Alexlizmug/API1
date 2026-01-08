using System;

namespace API1.Modeloak
{
    public class Eskaerak
    {
        public virtual int Id { get; set; }
        public virtual string? Izena { get; set; }
        public virtual float? Prezioa { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual int? Egoera { get; set; }
        public virtual int? ZerbitzuaId { get; set; }
        public virtual int ProduktuaId { get; set; }
    }
}
