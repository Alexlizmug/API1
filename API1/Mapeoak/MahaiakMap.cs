using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class MahaiakMap : ClassMap<Mahaiak>
    {
        public MahaiakMap()
        {
            Table("mahaiak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
            Map(x => x.Egoera).Column("egoera");
        }
    }
}
