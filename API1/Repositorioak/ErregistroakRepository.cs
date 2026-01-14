using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class ErregistroakRepository
    {
        private readonly NHSession _session;

        public ErregistroakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Erregistroak> GetAll()
        {
            return _session.Query<Erregistroak>().ToList();
        }

        public Erregistroak? GetById(int id)
        {
            return _session.Get<Erregistroak>(id);
        }

        public void Add(Erregistroak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Erregistroak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Erregistroak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
