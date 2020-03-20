using SoccerPrediction.Context;
using SoccerPrediction.Helper;
using SoccerPrediction.Model;
using SoccerPrediction.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerPrediction.BusinessLogic
{
    public class PeopleLogic : IGenericRepository<Person>
    {
        internal readonly SPXmlContext Context;

        public PeopleLogic()
        {
            using (var ctx = new SPXmlContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "xmlFile.xml")))
            {
                if (!ctx.HasPessimisticLock) ctx.Seed();
                Context = ctx;
            }
        }

        public bool Add(Person item)
        {
            Context.People.Add(item);
            return Context.SaveChanges();
        }

        public async Task<bool> AddAsync(Person item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Person item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Person item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Person item)
        {
            Context.People.Remove(item);
            return Context.SaveChanges();
        }

        public async Task<bool> DeleteAsync(Person item)
        {
            throw new NotImplementedException();
        }

        public Person Get(string username, string pass)
        {
            return Context.People.Where(p => p.Credentials.UserName == username && p.Credentials.EncryptedPassword == pass.GetHash()).FirstOrDefault();
        }

        public Person Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Person>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Person> GetAsync(string username, string pass)
        {
            throw new NotImplementedException();
        }

        public async Task<Person> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
