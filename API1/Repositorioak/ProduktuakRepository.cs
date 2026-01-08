using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class ProduktuakRepository
    {
        private readonly NHSession _session;

        public ProduktuakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Produktuak> GetAll()
        {
            return _session.Query<Produktuak>().ToList();
        }

        public Produktuak? GetById(int id)
        {
            return _session.Get<Produktuak>(id);
        }

        public void Add(Produktuak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(Produktuak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(Produktuak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
