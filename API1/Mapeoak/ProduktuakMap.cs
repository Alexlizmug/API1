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
            Map(x => x.stock_min).Column("stock_min");
            Map(x => x.stock_max).Column("stock_max");
            Map(x => x.Irudia).Column("irudia");
            Map(x => x.ProduktuenMotakId).Column("produktuen_motak_id");
        }
    }
}
