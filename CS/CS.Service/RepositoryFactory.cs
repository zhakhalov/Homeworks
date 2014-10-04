using CS.Repository.Abstract;
using CS.Repository.Concrete.SqlCurrencyRepositories;
using CS.Repository.Implementation.XmlCurrencyRepositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CS.Service
{
    public class RepositoryFactory
    {
        public ICurrencyRepository CreateCurrencyRepository(string repositoryName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["currencyRateEntities"].ConnectionString;
            string currencyFileName = ConfigurationManager.AppSettings["currencyFileName"];

            if (!Path.IsPathRooted(currencyFileName))
            {
                currencyFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), currencyFileName);
            }

            UnityContainer container = new UnityContainer();
            container.RegisterType<ICurrencyRepository, SqlCurrencyRepository>("sql", new InjectionConstructor(new InjectionParameter(connectionString)));
            container.RegisterType<ICurrencyRepository, XmlCurrencyRepository>("xml", new InjectionConstructor(new InjectionParameter(currencyFileName)));

            return container.Resolve<ICurrencyRepository>(repositoryName);
        }

        public IRateRepository CreateRateRepository(string repositoryName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["currencyRateEntities"].ConnectionString;
            string rateFileName = ConfigurationManager.AppSettings["rateFileName"];

            if (!Path.IsPathRooted(rateFileName))
            {
                rateFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), rateFileName);
            }

            UnityContainer container = new UnityContainer();
            container.RegisterType<IRateRepository, SqlRateRepository>("sql", new InjectionConstructor(new InjectionParameter(connectionString)));
            container.RegisterType<IRateRepository, XmlRateRepository>("xml", new InjectionConstructor(new InjectionParameter(rateFileName)));

            return container.Resolve<IRateRepository>(repositoryName);
        }
    }
}