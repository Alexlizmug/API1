using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using API1.Mapeoak;

namespace API1.Helpers
{
    public static class NHibernateHelper
    {
        public static ISessionFactory CreateSessionFactory(string connectionString)
        {
            try
            {
                return Fluently.Configure()
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
            }
            catch (Exception ex)
            {
                var msg = "NHibernate configuration error: " + ex.Message;
                if (ex.InnerException != null)
                    msg += "\nInner: " + ex.InnerException.Message;

                throw new Exception(msg, ex);
            }

        }
    
    }

}
