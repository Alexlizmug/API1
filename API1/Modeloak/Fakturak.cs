using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace API1.Modeloak
{
    public class Faktura
    {
        public virtual int Id { get; set; }
        public virtual int ZerbitzuaId { get; set; }
        public virtual float PrezioTotala { get; set; }
    }

}
