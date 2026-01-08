using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class ErreserbakMap : ClassMap<Erreserbak>
    {
        public ErreserbakMap()
        {
            Table("erreserbak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Data).Column("data");
            Map(x => x.Mota).Column("mota");
            Map(x => x.ErabiltzaileakId).Column("erabiltzaileak_id");
            Map(x => x.MahaiakId).Column("mahaiak_id");
        }
    }
}
