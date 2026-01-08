using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class MahaiakRepository
    {
        private readonly NHSession _session;

        public MahaiakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Mahaiak> GetAll()
        {
            return _session.Query<Mahaiak>().ToList();
        }

        public Mahaiak? GetById(int id)
        {
            return _session.Get<Mahaiak>(id);
        }

        public void Add(Mahaiak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Mahaiak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Mahaiak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
