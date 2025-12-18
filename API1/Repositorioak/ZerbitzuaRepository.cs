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

        public Zerbitzua GetById(int id)
        {
            return _session.Get<Zerbitzua>(id);
        }

        public void Add(Zerbitzua zerbitzua)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(zerbitzua);
                tx.Commit();
            }
        }

        public void Update(Zerbitzua zerbitzua)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(zerbitzua);
                tx.Commit();
            }
        }

        public void Delete(Zerbitzua zerbitzua)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(zerbitzua);
                tx.Commit();
            }
        }
    }
}
