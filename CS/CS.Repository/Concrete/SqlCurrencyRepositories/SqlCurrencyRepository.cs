using CS.Repository.Abstract;
using CS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CS.Repository.Concrete.SqlCurrencyRepositories
{
    public class SqlCurrencyRepository : ICurrencyRepository
    {
        readonly string _connectionString;

        #region Constructors

        public SqlCurrencyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion Constructors

        DbContext CreateDBContext()
        {
            return new DbContext(_connectionString);
        }

        public Entities.Currency GetByCode(string code)
        {
            Entities.Currency currency = null;

            using (DbContext context = CreateDBContext())
            {
                currency = context.Set<Entities.Currency>().First(r => code == r.Code);                
            }

            return currency;
        }

        public List<Concrete.Currency> GetAll()
        {
            List<Concrete.Currency> list = new List<Concrete.Currency>();

            using (DbContext context = CreateDBContext())
            {
                List<Entities.Currency> raw = context.Set<Entities.Currency>().ToList();
                raw.ForEach(r => list.Add(new Concrete.Currency
                {
                    Name = r.Name,
                    Code = r.Code
                }));
            }
            return list;
        }

        public void Add(Concrete.Currency item)
        {
            using (DbContext context = CreateDBContext())
            {
                context.Set<Entities.Currency>().Add(new Entities.Currency
                    {
                        Code = item.Code,
                        Name = item.Name
                    });
                context.SaveChanges();
            }
        }
    }
}
