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

        public Eskaerak? GetById(int id)
        {
            return _session.Get<Eskaerak>(id);
        }

        public List<Eskaerak> GetByZerbitzuaId(int zerbitzuaId)
        {
            return _session.Query<Eskaerak>()
                           .Where(e => e.Zerbitzua.Id == zerbitzuaId)
                           .ToList();
        }


        public void Add(Eskaerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Eskaerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Eskaerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
