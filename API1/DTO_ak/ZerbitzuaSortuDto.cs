namespace API1.DTO_ak
{
    public class ZerbitzuaSortuDto
    {
        public DateTime Data { get; set; }
        public int? ErreserbaId { get; set; }
        public int? MahaiakId { get; set; }
        public float PrezioTotala { get; set; }
        public IList<EskaerakSortuDto> Eskaerak { get; set; } = new List<EskaerakSortuDto>();
    }
}
