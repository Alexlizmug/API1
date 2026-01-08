using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class ErregistroakMap : ClassMap<Erregistroak>
    {
        public ErregistroakMap()
        {
            Table("erregistroak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Erabiltzailea).Column("erabiltzailea");
            Map(x => x.Pasahitza).Column("pasahitza");
            Map(x => x.Secret).Column("secret");
        }
    }
}
