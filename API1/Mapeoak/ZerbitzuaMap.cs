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

            Map(x => x.Izena).Column("izena");
            Map(x => x.Prezioa).Column("prezioa");
        }
    }
}

