using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class PlateraMotakRepository
    {
        private readonly NHSession _session;

        public PlateraMotakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<PlateraMotak> GetAll()
        {
            return _session.Query<PlateraMotak>().ToList();
        }

        public PlateraMotak? GetById(int id)
        {
            return _session.Get<PlateraMotak>(id);
        }

        public void Add(PlateraMotak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(PlateraMotak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(PlateraMotak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
