using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class ProduktuenMotakMap : ClassMap<ProduktuenMotak>
    {
        public ProduktuenMotakMap()
        {
            Table("produktuen_motak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
        }
    }
}
