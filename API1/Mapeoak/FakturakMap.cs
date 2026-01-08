using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class FakturakMap : ClassMap<Fakturak>
    {
        public FakturakMap()
        {
            Table("fakturak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.PrezioTotala).Column("prezio_totala");
            Map(x => x.Sortuta).Column("sortuta");
            Map(x => x.Path).Column("path");
        }
    }
}
