using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class EskaerakRepository
    {
        private readonly NHSession _session;

        public EskaerakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Eskaerak> GetAll()
        {
            return _session.Query<Eskaerak>().ToList();
        }

        public Eskaerak GetById(int id)
        {
            return _session.Get<Eskaerak>(id);
        }

        public void Add(Eskaerak eskaera)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(eskaera);
                tx.Commit();
            }
        }

        public void Update(Eskaerak eskaera)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(eskaera);
                tx.Commit();
            }
        }

        public void Delete(Eskaerak eskaera)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(eskaera);
                tx.Commit();
            }
        }
    }
}

