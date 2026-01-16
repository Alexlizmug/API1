namespace API1.DTO_ak
{
    public class FakturakDto
    {
        public virtual int Id { get; set; }
        public virtual int ZerbitzuaId { get; set; }
        public virtual float? PrezioTotala { get; set; }
        public virtual bool Sortuta { get; set; }
        public virtual string Path { get; set; } = string.Empty;
    }

}