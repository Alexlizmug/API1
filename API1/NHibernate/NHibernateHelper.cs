using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using API1.Mapeoak;

namespace API1.Helpers
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory CreateSessionFactory(string connectionString)
        {
            if (_sessionFactory != null)
                return _sessionFactory;

            try
            {
                _sessionFactory = Fluently.Configure()
                    .Database(
                        MySQLConfiguration.Standard
                            .ConnectionString(connectionString)
                            .ShowSql()
                    )
                    .Mappings(m =>
                    {
                        m.FluentMappings.AddFromAssemblyOf<LangileakMap>();
                    })
                    .BuildSessionFactory();

                return _sessionFactory;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating session factory: " + ex.Message);
                throw;
            }
        }
<<<<<<< Updated upstream:API1/NHibernate/NHibernateHelper.cs
=======

        public static NHibernate.ISession OpenSession()
        {
            if (_sessionFactory == null)
                throw new InvalidOperationException(
                    "SessionFactory not initialized. Call CreateSessionFactory() first."
                );

            return _sessionFactory.OpenSession();
        }
>>>>>>> Stashed changes:API1/Helpers/NHibernateHelper.cs
    }
}
