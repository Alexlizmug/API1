using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class LangileakRepository
    {
        private readonly NHSession _session;

        public LangileakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Langileak> GetAll()
        {
            return _session.Query<Langileak>().ToList();
        }

        public Langileak? GetById(int id)
        {
            return _session.Get<Langileak>(id);
        }

        public void Add(Langileak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Langileak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Langileak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
