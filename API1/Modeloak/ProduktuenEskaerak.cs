namespace API1.Modeloak
{
    public class ProduktuenEskaerak
    {
        public virtual int Id { get; set; }
        public virtual int? Kantitatea { get; set; }
        public virtual int? KantMax { get; set; }
        public virtual int? KantMin { get; set; }
        public virtual int ProduktuakId { get; set; }
    }
}
