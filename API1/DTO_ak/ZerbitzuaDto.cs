namespace API1.DTO_ak
{
    public class ZerbitzuaDto
    {
        public int Id { get; set; }
        public float? PrezioTotala { get; set; }
        public DateTime? Data { get; set; }
        public int? ErreserbaId { get; set; }
        public int? MahaiakId { get; set; }

        public List<EskaerakDto> Eskaerak { get; set; } = new();
    }
}
