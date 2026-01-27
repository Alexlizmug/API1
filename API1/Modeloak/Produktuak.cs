namespace API1.Modeloak
{
    public class Produktuak
    {
        public virtual int Id { get; set; }
        public virtual string? Izena { get; set; }
        public virtual float? Prezioa { get; set; }
        public virtual int? Stock { get; set; }
        public virtual int? stock_min { get; set; }
        public virtual int? stock_max { get; set; }
        public virtual string? Irudia { get; set; }
        public virtual int ProduktuenMotakId { get; set; }
    }
}
