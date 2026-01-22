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

            Map(x => x.Data).Column("data").Not.Nullable();
            Map(x => x.Mota).Column("mota").Not.Nullable();
            Map(x => x.ErabiltzaileakId).Column("erabiltzaileak_id").Nullable();
            Map(x => x.MahaiakId).Column("mahaiak_id").Not.Nullable();
        }
    }
}
