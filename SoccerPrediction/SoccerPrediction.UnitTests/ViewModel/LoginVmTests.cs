using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SoccerPrediction.BusinessLogic;
using SoccerPrediction.Context;
using SoccerPrediction.Model;
using SoccerPrediction.Repository;
using SoccerPrediction.ViewModel;
using SoccerPrediction.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerPrediction.UnitTests.ViewModel
{
    class LoginVmTests
    {
        [SetUp]
        public void Setup()
        {
            ///Mock injizieren
            ServiceContainer.ServiceInstance.AddService<LoginWindowServiceMock>(new LoginWindowServiceMock());

        }

        [Test]
        public void InitVmMustSetDisplayText()
        {
            var options = new DbContextOptionsBuilder<SPDbContext>().UseInMemoryDatabase("inMemoryDb.db").Options;
            var ctx = new SPDbContext(options);
            var repo = new GenericRepository<Person>(ctx);
            var encryptedPassword = Helper.PasswordHelper.GetHash("password1");
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser1", EncryptedPassword = encryptedPassword } });
            LoginViewModel sut = new LoginViewModel("TestText", new PeopleLogic(repo));

            Assert.IsNotNull(sut.DisplayText);

           
            Assert.IsNotNull(sut.DisplayText);
            Assert.AreEqual("TestText", sut.DisplayText);
        }



        [Test]
        public void UsernameValidationTest()
        {
            var options = new DbContextOptionsBuilder<SPDbContext>().UseInMemoryDatabase("inMemoryDb.db").Options;
            var ctx = new SPDbContext(options);
            var repo = new GenericRepository<Person>(ctx);
            var encryptedPassword = Helper.PasswordHelper.GetHash("password1");
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser1", EncryptedPassword = encryptedPassword } });
            LoginViewModel sut = new LoginViewModel("unitTest", new PeopleLogic(repo));
            Assert.AreEqual(3, sut.ValidationErrors().Count);
            sut.UserName = "testuser1";
            Assert.AreEqual(1, sut.ValidationErrors().Count);
            Assert.IsTrue(sut.ValidationErrors().First().MemberNames.First() == nameof(sut.Password));
        }

        [Test]
        public void PasswordValidationTest()
        {
            var options = new DbContextOptionsBuilder<SPDbContext>().UseInMemoryDatabase("inMemoryDb.db").Options;
            var ctx = new SPDbContext(options);
            var repo = new GenericRepository<Person>(ctx);
            var encryptedPassword = Helper.PasswordHelper.GetHash("password1");
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser1", EncryptedPassword = encryptedPassword } });
            LoginViewModel sut = new LoginViewModel("unitTest", new PeopleLogic(repo));
            Assert.AreEqual(3, sut.ValidationErrors().Count);
            sut.Password = "password1";
            Assert.AreEqual(2, sut.ValidationErrors().Count);
            Assert.IsTrue(sut.ValidationErrors().First().MemberNames.First() == nameof(sut.UserName));
        }

        [Test]
        public async Task CanLoginIfDataIsCorrect()
        {
            var options = new DbContextOptionsBuilder<SPDbContext>().UseInMemoryDatabase("inMemoryDb.db").Options;
            var ctx = new SPDbContext(options);
            var repo = new GenericRepository<Person>(ctx);
            var encryptedPassword = Helper.PasswordHelper.GetHash("password1");
            repo.AddOrUpdate(new Model.Person { FirstName = "Test", Credentials = new Model.AccessData() { UserName = "testuser1", EncryptedPassword = encryptedPassword } });
            repo.AddOrUpdate(new Model.Person { FirstName = "Test", Credentials = new Model.AccessData() { UserName = "testuser2", EncryptedPassword = encryptedPassword } });
            repo.AddOrUpdate(new Model.Person { FirstName = "Test", Credentials = new Model.AccessData() { UserName = "testuser3", EncryptedPassword = encryptedPassword } });


            LoginViewModel sut = new LoginViewModel("unitTest",new PeopleLogic(repo));

            
            //Assert.IsFalse(sut.CanLogin());
            sut.UserName = "testuser1";
            sut.Password = "password";
            Assert.IsFalse(await sut.CanLogin());

            sut.UserName = "testuser1";
            sut.Password = "password1";
            Assert.IsTrue(await sut.CanLogin());

            IWindowService _fakeWin = ServiceContainer.GetService<IWindowService>();
            Assert.IsTrue(sut.LogInCommand.CanExecute(null));
            //sut.LogInCommand.Execute(null);
        }

        [Test]
        public void CheckIfCanLoginFalseIfMissingData()
        {
            var options = new DbContextOptionsBuilder<SPDbContext>().UseInMemoryDatabase("inMemoryDb.db").Options;
            var ctx = new SPDbContext(options);
            var repo = new GenericRepository<Person>(ctx);
            var encryptedPassword = Helper.PasswordHelper.GetHash("password1");
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser1", EncryptedPassword = encryptedPassword } });
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser2", EncryptedPassword = encryptedPassword } });
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser3", EncryptedPassword = encryptedPassword } });


            LoginViewModel sut = new LoginViewModel("unitTest", new PeopleLogic(repo));


            //Assert.IsFalse(sut.CanLogin());
            sut.UserName = "";
            sut.Password = "";
            Assert.IsFalse(sut.LogInCommand.CanExecute(null));

            sut.UserName = "testuser1";
            sut.Password = "password1";
            Assert.IsTrue(sut.LogInCommand.CanExecute(null));

          
        }


        [Test]
        public void CheckWrongUserDataMustGenerateValidationErrors()
        {
            var options = new DbContextOptionsBuilder<SPDbContext>().UseInMemoryDatabase("inMemoryDb.db").Options;
            var ctx = new SPDbContext(options);
            var repo = new GenericRepository<Person>(ctx);
            var encryptedPassword = Helper.PasswordHelper.GetHash("password1");
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser1", EncryptedPassword = encryptedPassword } });
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser2", EncryptedPassword = encryptedPassword } });
            repo.AddOrUpdate(new Model.Person { Credentials = new Model.AccessData() { UserName = "testuser3", EncryptedPassword = encryptedPassword } });


            LoginViewModel sut = new LoginViewModel("unitTest", new PeopleLogic(repo));


            //Ohne Daten gibt Error
            sut.UserName = "";
            sut.Password = "";
            Assert.IsTrue(sut.IsValid);

            //Usernamen mit weniger als drei zeichen gibt es sowieso nicht also ist es auch nicht valid
            sut.UserName = "te";
            sut.Password = "password1";
            Assert.IsTrue(sut.IsValid);

            Assert.AreEqual("Username ist zu kurz", sut.ValidationErrors().First().ErrorMessage);
            Assert.AreEqual(nameof(sut.UserName), sut.ValidationErrors().First().MemberNames.First());



        }

    }
}
