namespace API1.DTO_ak
{
    public class ProduktuakDto
    {
        public int Id { get; set; }
        public string? Izena { get; set; }
        public float? Prezioa { get; set; }
        public int? Stock { get; set; }
        public int? stock_min { get; set; }
        public int? stock_max { get; set; }
        public string? Irudia { get; set; }
        public int ProduktuenMotakId { get; set; }
    }
}
