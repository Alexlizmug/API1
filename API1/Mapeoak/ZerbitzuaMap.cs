using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class ZerbitzuaMap : ClassMap<Zerbitzua>
    {
        public ZerbitzuaMap()
        {
            Table("zerbitzua");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.PrezioTotala).Column("prezioTotala");
            Map(x => x.Data).Column("data");
            Map(x => x.ErreserbaId).Column("erreserba_id");
            Map(x => x.MahaiakId).Column("mahaiak_id");
            Map(x => x.Ordainduta).Column("ordainduta");

            HasMany(x => x.Eskaerak)
                   .KeyColumn("zerbitzua_id")
                   .Cascade.AllDeleteOrphan()
                   .Inverse()
                   .Not.LazyLoad();
        }
    }
}
