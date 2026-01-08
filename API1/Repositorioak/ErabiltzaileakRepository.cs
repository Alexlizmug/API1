using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class ErabiltzaileakRepository
    {
        private readonly NHSession _session;

        public ErabiltzaileakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Erabiltzaileak> GetAll()
        {
            return _session.Query<Erabiltzaileak>().ToList();
        }

        public Erabiltzaileak? GetById(int id)
        {
            return _session.Get<Erabiltzaileak>(id);
        }

        public void Add(Erabiltzaileak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Erabiltzaileak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Erabiltzaileak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
