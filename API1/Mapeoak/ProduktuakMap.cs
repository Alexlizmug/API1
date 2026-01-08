using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class ProduktuakMap : ClassMap<Produktuak>
    {
        public ProduktuakMap()
        {
            Table("produktuak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
            Map(x => x.Prezioa).Column("prezioa");
            Map(x => x.Stock).Column("stock");
            Map(x => x.IrudiaPath).Column("irudia_path");
            Map(x => x.ProduktuenMotakId).Column("produktuen_motak_id");
        }
    }
}
