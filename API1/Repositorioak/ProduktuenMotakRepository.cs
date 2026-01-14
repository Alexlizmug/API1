using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class ProduktuenMotakRepository
    {
        private readonly NHSession _session;

        public ProduktuenMotakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<ProduktuenMotak> GetAll()
        {
            return _session.Query<ProduktuenMotak>().ToList();
        }

        public ProduktuenMotak? GetById(int id)
        {
            return _session.Get<ProduktuenMotak>(id);
        }

        public void Add(ProduktuenMotak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(ProduktuenMotak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(ProduktuenMotak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
