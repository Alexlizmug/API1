namespace API1.Modeloak
{
    public class Faktura
    {
        public virtual int Id { get; set; }
        public virtual int ZerbitzuaId { get; set; }
        public virtual float? PrezioTotala { get; set; }
        public virtual bool Sortuta { get; set; }
        public virtual string Path { get; set; }
    }

}
