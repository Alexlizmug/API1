using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class ProduktuenEskaerakRepository
    {
        private readonly NHSession _session;

        public ProduktuenEskaerakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<ProduktuenEskaerak> GetAll()
        {
            return _session.Query<ProduktuenEskaerak>().ToList();
        }

        public ProduktuenEskaerak? GetById(int id)
        {
            return _session.Get<ProduktuenEskaerak>(id);
        }

        public void Add(ProduktuenEskaerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(ProduktuenEskaerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(ProduktuenEskaerak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
