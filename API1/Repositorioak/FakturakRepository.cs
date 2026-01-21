using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class FakturakRepository
    {
        private readonly NHSession _session;

        public FakturakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Faktura> GetAll()
        {
            return _session.Query<Faktura>().ToList();
        }

        public Faktura? GetById(int id)
        {
            return _session.Get<Faktura>(id);
        }

        public void Insert(Faktura faktura) {
            using var tx = _session.BeginTransaction();
            _session.Save(faktura);
            tx.Commit();
        }

        public void Update(Faktura entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }
        public Faktura GetByZerbitzuaId(int zerbitzuaId)
        {
            return _session.Query<Faktura>()
                           .FirstOrDefault(f => f.ZerbitzuaId == zerbitzuaId);
        }


        public void Delete(Faktura entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
