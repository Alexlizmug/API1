using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class ProduktuenEskaerakMap : ClassMap<ProduktuenEskaerak>
    {
        public ProduktuenEskaerakMap()
        {
            Table("produktuen_eskaerak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Kantitatea).Column("kantitatea");
            Map(x => x.KantMax).Column("kant_max");
            Map(x => x.KantMin).Column("kant_min");
            Map(x => x.ProduktuakId).Column("produktuak_id");
        }
    }
}
