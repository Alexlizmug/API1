using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class FakturakMap : ClassMap<Faktura>
    {
        public FakturakMap()
        {
            Table("fakturak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();
            Map(x => x.ZerbitzuaId).Column("zerbitzua_id");
            Map(x => x.PrezioTotala).Column("prezio_totala");
        }
    }
}
