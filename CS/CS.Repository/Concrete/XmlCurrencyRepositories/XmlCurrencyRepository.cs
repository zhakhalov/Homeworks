using CS.Repository.Abstract;
using CS.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CS.Repository.Implementation.XmlCurrencyRepositories
{
    public class XmlCurrencyRepository : ICurrencyRepository
    {
        private readonly string _fileName;

        public XmlCurrencyRepository(string fileName)
        {
            _fileName = fileName;
        }

        public void Add(Currency item)
        {
            List<Currency> list = GetAll();
            list.Add(item);

            using(FileStream stream = File.OpenWrite(_fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Currency>));
                serializer.Serialize(stream, list);
            }
        }

        public void Remove(Currency item)
        {
            List<Currency> list = GetAll();

            if (!list.Contains(item))
            {
                return;
            }

            list.Remove(item);

            using (FileStream stream = File.OpenWrite(_fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Currency>));
                serializer.Serialize(stream, list);
            }
        }

        public List<Currency> GetAll()
        {          
            List<Currency> currencies = new List<Currency>();

            if (File.Exists(_fileName))
            {
                using (FileStream stream = File.OpenRead(_fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Currency>));
                    currencies = (List<Currency>)serializer.Deserialize(stream);
                }
            }

            return currencies;
        }
    }
}
