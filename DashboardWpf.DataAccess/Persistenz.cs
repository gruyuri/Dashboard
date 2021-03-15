using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Context;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardWpf.DataAccess
{
    public interface IEntity
    {
        Int64? primaryKey { get; }
    }

    public class Persistenz: IDisposable
    {
        private static ISessionFactory factory = null;
        public static String factorySemaphore = "factorySemaphore";

        private static ICollection<IConvention> _nhibernateConventions = new List<IConvention>();

        public static ICollection<IConvention> nhibernateConventions => _nhibernateConventions;

        public static ISession CurrentSession => SessionFactory.GetCurrentSession();

        public ISession Session { get; protected set; }

        public Persistenz()
        {
            lock (factorySemaphore)
            {
                ISession tmp = SessionFactory.OpenSession();
                tmp.FlushMode = FlushMode.Commit;
                CurrentSessionContext.Bind(tmp);

                Session = tmp;
            }

            Session.BeginTransaction();
        }

        public void Commit() => Session.Transaction.Commit();

        public static bool IsAssignableFrom<T>(object source) where T : class
        {
            return (source is INHibernateProxy)
                ? (NHibernateUtil.GetClass(source).IsAssignableFrom(typeof(T)))
                : (source.GetType().IsAssignableFrom(typeof(T)));
        }

        public static T CastAs<T>(object source) where T : class
        {
            return ((source is INHibernateProxy) ? ((INHibernateProxy)source).HibernateLazyInitializer.GetImplementation() : source) as T;
        }

        public static FluentConfiguration Configuration
        {
            get
            {
                //String dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["PINPeristenz.dbUrlKey"];
                string dbConnectionString = "Host=localhost;Database=dashboard;Username=postgres;Password=root;Persist Security Info=True";
                //log.Info($"Konfigurationseintrag für die DB URL: '{dbUrlSchluessel}'");
                FluentConfiguration config = Fluently.Configure().Database(
                    PostgreSQLConfiguration.Standard.ConnectionString(dbConnectionString.ToString()))
                    .ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"))
                    .ExposeConfiguration(c => c.Properties.Add("current_session_context_class",
                     "thread_static"))
                    .ExposeConfiguration(c => c.Properties["show_sql"] = "false")
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IEntity>())
                    .ExposeConfiguration(c => c.Properties["use_reflection_optimizer"] = "true")
                .ExposeConfiguration(c => c.Properties["adonet.batch_size"] = "100");

                config.Cache(c => c.ProviderClass<NHibernate.Cache.HashtableCacheProvider>().UseSecondLevelCache());

                foreach (IConvention convention in nhibernateConventions)
                {
                    config.Mappings(m => m.FluentMappings.Conventions.Add(convention));
                }
                config.Mappings(m => m.FluentMappings.Conventions.Add(new PgSQLConvention()));

                return config;
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                lock (factorySemaphore)
                {
                    if (factory == null)
                    {
                        NHibernate.Cfg.Configuration config = Configuration.BuildConfiguration();
                        factory = config.BuildSessionFactory();
                    }
                }

                return factory;
            }
        }

        public static void WithSessionTx(Action<ISession> code)
        {
            using (var p = new Persistenz())
            {
                code.Invoke(p.Session);

                p.Commit();
            }
        }

        public static T WithSessionTxRs<T>(Func<ISession, T> code)
        {
            T result = default(T);

            using (var p = new Persistenz())
            {
                result = code.Invoke(p.Session);

                p.Commit();
            }

            return result;
        }

        void IDisposable.Dispose()
        {
            lock (factorySemaphore)
            {
                if (Session.Transaction != null && Session.Transaction.IsActive)
                {
                    try
                    {
                        Session.Transaction.Rollback();
                        //log.Warn("Rolled Transaction back");

                    }
                    catch (Exception ce)
                    {
                        //log.Fatal($"Rollback failed! - {ce.Message}", ce);
                    }
                }

                CurrentSessionContext.Unbind(SessionFactory);
                if (Session != null && Session.IsOpen)
                {
                    try
                    {
                        Session.Close();
                    }
                    catch (Exception ce)
                    {
                        //log.Fatal($"Session Close failed! - {ce.Message}", ce);
                    }
                }
            }

            Session = null;
        }

    }

    public class PgSQLConvention : FluentNHibernate.Conventions.IIdConvention
    {
        //protected ILog log = LogManager.GetLogger(typeof(PgSQLConvention));

        void FluentNHibernate.Conventions.IConvention<FluentNHibernate.Conventions.Inspections.IIdentityInspector, FluentNHibernate.Conventions.Instances.IIdentityInstance>.Apply(FluentNHibernate.Conventions.Instances.IIdentityInstance instance)
        {
            instance.GeneratedBy.Sequence("hibernate_sequence");
            instance.Default("nextval('hibernate_sequence')");
        }
    }
}
