public class Zerbitzua
{
    public virtual int Id { get; set; }
    public virtual float PrezioTotala { get; set; }
    public virtual DateTime? Data { get; set; }
    public virtual int? ErreserbaId { get; set; }
    public virtual int? MahaiakId { get; set; }
    public virtual bool Ordainduta { get; set; }
    public virtual IList<Eskaerak> Eskaerak { get; set; } = new List<Eskaerak>();
}
