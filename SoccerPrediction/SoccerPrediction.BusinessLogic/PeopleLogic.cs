using SoccerPrediction.Helper;
using SoccerPrediction.Model;
using SoccerPrediction.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("SoccerPrediction.UnitTests")]
namespace SoccerPrediction.BusinessLogic
{

    public class PeopleLogic : BusinessLogicBase
    {

        internal readonly GenericRepository<Person> PersonRep;

        public PeopleLogic()
        {
            PersonRep = new GenericRepository<Person>();
        }

        internal PeopleLogic(GenericRepository<Person> personRep)
        {
            PersonRep = personRep ?? throw new ArgumentNullException(nameof(personRep));
        }

        #region Get Methods

        public async Task<bool> ArePersonCredentialsCorrect(string username, SecureString pass)
        {
            var query = PersonRep.GetAll(true).Include(c => c.Credentials).Where(p => p.Credentials.UserName == username);
            var person = await PersonRep.FindByAsync(query);
            if (person.Credentials.EncryptedPassword == pass.Unsecure().GetHash()) return true;
            return false;
        }



        public async Task<Person> GetPersonByName(string username)
        {
            return await PersonRep.FindByAsync(GetCompleteData(p => p.Credentials.UserName == username));
        }

        public async Task<Person> GetPersonById(Guid id)
        {
            return await PersonRep.FindByAsync(PersonRep.GetAll(true).Where(p => p.Id == id));
        }

        #endregion

        #region Helper Methods

        internal IQueryable<Person> GetCompleteData(Expression<Func<Person, bool>> predicate)
        {
            return PersonRep.GetAll(true).Where(predicate).Include(p => p.Credentials);
        }

        public async Task<bool> HasEntriesAsync()
        {
            return await PersonRep.AnyAsync(true);
        }

        public async Task<bool> EnsureDbIsCreated(bool seed)
        {
            return await PersonRep.CreateDbIfNotExist(seed).ConfigureAwait(true);
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            PersonRep?.Dispose();
        }

        #endregion
    }
}
