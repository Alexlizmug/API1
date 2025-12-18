using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class ErreserbakRepository
    {
        private readonly NHSession _session;

        public ErreserbakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Erreserbak> GetAll()
        {
            return _session.Query<Erreserbak>().ToList();
        }

        public Erreserbak GetById(int id)
        {
            return _session.Get<Erreserbak>(id);
        }

        public void Add(Erreserbak erreserba)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(erreserba);
                tx.Commit();
            }
        }

        public void Update(Erreserbak erreserba)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(erreserba);
                tx.Commit();
            }
        }

        public void Delete(Erreserbak erreserba)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(erreserba);
                tx.Commit();
            }
        }
    }
}

