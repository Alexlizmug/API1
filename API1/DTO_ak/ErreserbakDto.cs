using System;

namespace API1.DTO_ak
{
    public class ErreserbakDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public bool Mota { get; set; }
        public int? ErabiltzaileakId { get; set; }
        public int MahaiakId { get; set; }
    }
}
