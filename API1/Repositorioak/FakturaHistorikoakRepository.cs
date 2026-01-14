using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class FakturaHistorikoakRepository
    {
        private readonly NHSession _session;

        public FakturaHistorikoakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<FakturaHistorikoak> GetAll()
        {
            return _session.Query<FakturaHistorikoak>().ToList();
        }

        public FakturaHistorikoak? GetById(int id)
        {
            return _session.Get<FakturaHistorikoak>(id);
        }

        public void Add(FakturaHistorikoak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(entity);
                tx.Commit();
            }
        }

        public void Update(FakturaHistorikoak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(entity);
                tx.Commit();
            }
        }

        public void Delete(FakturaHistorikoak entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();
            }
        }
    }
}
