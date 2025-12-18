using System.Collections.Generic;
using System.Linq;
using API1.Modeloak;
using NHibernate.Linq;
using NHSession = NHibernate.ISession;

namespace API1.Repositorioak
{
    public class LangileakRepository
    {
        // Usamos el alias NHSession para no chocar con Http.ISession
        private readonly NHSession _session;

        public LangileakRepository(NHSession session)
        {
            _session = session;
        }

        public IList<Langileak> GetAll()
        {
            return _session.Query<Langileak>().ToList();
        }

        public Langileak GetById(int id)
        {
            return _session.Get<Langileak>(id);
        }

        public void Add(Langileak langilea)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(langilea);
                tx.Commit();
            }
        }

        public void Update(Langileak langilea)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(langilea);
                tx.Commit();
            }
        }

        public void Delete(Langileak langilea)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(langilea);
                tx.Commit();
            }
        }

        public Langileak GetByCredentials(string erabiltzailea, string pasahitza)
        {
            return _session.Query<Langileak>()
                           .FirstOrDefault(l =>
                               l.Erabiltzailea == erabiltzailea &&
                               l.Pasahitza == pasahitza);
        }
    }
}
