using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class LangileakMap : ClassMap<Langileak>
    {
        public LangileakMap()
        {
            Table("langileak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
            Map(x => x.Abizena).Column("abizena");
            Map(x => x.Erabiltzailea).Column("erabiltzailea");
            Map(x => x.Pasahitza).Column("pasahitza");
            Map(x => x.Email).Column("email");
            Map(x => x.Telefonoa).Column("telefonoa");
            Map(x => x.Baimena).Column("baimena");
            Map(x => x.MahaiakId).Column("mahaiak_id");
        }
    }
}
