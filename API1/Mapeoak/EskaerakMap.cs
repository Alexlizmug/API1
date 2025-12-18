using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class EskaerakMap : ClassMap<Eskaerak>
    {
        public EskaerakMap()
        {
            Table("eskaerak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
            Map(x => x.Data).Column("data");
            Map(x => x.Prezioa).Column("prezioa");
            Map(x => x.MahaiakId).Column("mahaiak_id");
            Map(x => x.Egoera).Column("egoera");
        }
    }
}
