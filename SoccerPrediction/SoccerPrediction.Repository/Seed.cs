using Microsoft.EntityFrameworkCore;
using SoccerPrediction.Context;
using SoccerPrediction.Helper;
using SoccerPrediction.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SoccerPrediction.Repository
{
    public class Seed
    {
        public static async Task CreatePeopleOnNewDb(SPDbContext context)
        {
            Debug.WriteLine("Start seeding....");
            if (!await context.People.AnyAsync().ConfigureAwait(true))
            {
                context.People.AddRange
                    (
                    new Person
                    {
                        FirstName = "Hans",
                        LastName = "Muster",
                        Credentials = new AccessData
                        {
                            UserName = "hmuster",
                            EncryptedPassword = "pass".GetHash(),
                            IsAdmin = true
                        }
                    },
                    new Person
                    {
                        FirstName = "Herbert",
                        LastName = "Muster2",
                        Credentials = new AccessData
                        {
                            UserName = "hmuster2",
                            EncryptedPassword = "pass2".GetHash(),
                            IsAdmin = false
                        }
                    }
                    );
                context.Teams.AddRange
                    (
                    new Team() { Name = "Eintracht Frankfurt" },
                    new Team() { Name = "Schalke 04" },
                    new Team() { Name = "Borussia Dortmund" }
                    );
                
                if (await context.SaveChangesAsync().ConfigureAwait(true) == 0)
                        throw new Exception("Es wurde nichts gespeichert, sollte aber unbedingt in diesem Fall passiert sein!!");
                }

            Debug.WriteLine("Seeding finished!");
        }
    }
}
