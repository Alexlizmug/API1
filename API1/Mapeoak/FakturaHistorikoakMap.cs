using FluentNHibernate.Mapping;
using API1.Modeloak;

namespace API1.Mapeoak
{
    public class FakturaHistorikoakMap : ClassMap<FakturaHistorikoak>
    {
        public FakturaHistorikoakMap()
        {
            Table("faktura_historikoak");

            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Identity();

            Map(x => x.Izena).Column("izena");
            Map(x => x.PrezioTotala).Column("prezio_totala");
            Map(x => x.Data).Column("data");
            Map(x => x.EskaerenKutxaId).Column("eskaeren_kutxa_id");
        }
    }
}
