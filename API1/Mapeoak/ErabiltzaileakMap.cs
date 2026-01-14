using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class ErabiltzaileakMap : ClassMap<Erabiltzaileak>
    {
        public ErabiltzaileakMap()
        {
            Table("erabiltzaileak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
            Map(x => x.Email).Column("email");
            Map(x => x.Pasahitza).Column("pasahitza");
            Map(x => x.Telefonoa).Column("telefonoa");
            Map(x => x.Abizena).Column("abizena");
        }
    }
}
