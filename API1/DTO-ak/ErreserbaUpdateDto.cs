using System;

namespace API1.DTO_ak
{
    public class ErreserbaUpdateDto
    {
        public string Izena { get; set; }
        public DateTime? Data { get; set; }
        public string Mota { get; set; }
        public int? ErabiltzaileakId { get; set; }
        public int? MahaiaId { get; set; }
    }
}
