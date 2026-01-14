using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class PlaterakMap : ClassMap<Platerak>
    {
        public PlaterakMap()
        {
            Table("platerak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
            Map(x => x.Mota).Column("mota");
            Map(x => x.Perezioa).Column("perezioa");
            Map(x => x.PlateraMotakId).Column("platera_motak_id");
        }
    }
}
