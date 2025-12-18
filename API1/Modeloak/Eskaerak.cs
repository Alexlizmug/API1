using System;

namespace API1.Modeloak
{
    public class Eskaerak
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual decimal Prezioa { get; set; }
        public virtual int? MahaiakId { get; set; }   // kolumna: mahaiak_id
        public virtual string Egoera { get; set; }
    }
}

