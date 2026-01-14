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

        public IList<Fakturak> GetAll()
        {
            return _session.Query<Fakturak>().ToList();
        }

        public Fakturak? GetById(int id)
        {
            return _session.Get<Fakturak>(id);
        }

        public void Add(Fakturak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Fakturak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Fakturak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
