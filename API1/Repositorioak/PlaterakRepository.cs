using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class PlaterakRepository
    {
        private readonly NHSession _session;

        public PlaterakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Platerak> GetAll()
        {
            return _session.Query<Platerak>().ToList();
        }

        public Platerak? GetById(int id)
        {
            return _session.Get<Platerak>(id);
        }

        public void Add(Platerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Platerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Platerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
