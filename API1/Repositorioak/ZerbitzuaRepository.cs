using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class ZerbitzuaRepository
    {
        private readonly NHSession _session;

        public ZerbitzuaRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Zerbitzua> GetAll()
        {
            return _session.Query<Zerbitzua>().ToList();
        }

        public Zerbitzua? GetById(int id)
        {
            return _session.Get<Zerbitzua>(id);
        }

        public void Add(Zerbitzua entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Zerbitzua entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }
        public IList<Eskaerak> GetEskaerakByZerbitzuaId(int zerbitzuaId)
        {
            return _session.Query<Eskaerak>()
                           .Where(e => e.Zerbitzua.Id == zerbitzuaId)
                           .ToList();
        }


        public void Delete(Zerbitzua entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
