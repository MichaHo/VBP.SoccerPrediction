using NUnit.Framework;
using SoccerPrediction.ViewModel;
using SoccerPrediction.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            LoginViewModel sut = new LoginViewModel();
            Assert.IsNotNull(sut.DisplayText);

            sut = new LoginViewModel("TestText");
            Assert.IsNotNull(sut.DisplayText);
            Assert.AreEqual("TestText", sut.DisplayText);
        }



        [Test]
        public void UsernameValidationTest()
        {
            LoginViewModel sut = new LoginViewModel();
            Assert.AreEqual(2, sut.ValidationErrors().Count);
            sut.UserName = "TestUser";
            Assert.AreEqual(1, sut.ValidationErrors().Count);
            Assert.IsTrue(sut.ValidationErrors().First().MemberNames.First() == nameof(sut.Password));
        }

        [Test]
        public void PasswordValidationTest()
        {
            LoginViewModel sut = new LoginViewModel();
            Assert.AreEqual(2, sut.ValidationErrors().Count);
            sut.Password = "pass";
            Assert.AreEqual(1, sut.ValidationErrors().Count);
            Assert.IsTrue(sut.ValidationErrors().First().MemberNames.First() == nameof(sut.UserName));
        }

        [Test]
        public void CanLoginIfDataIsCorrect()
        {
            LoginViewModel sut = new LoginViewModel();
            Assert.IsFalse(sut.CanLogin());
            sut.UserName = "hmuster";
            sut.Password = "pass";
            Assert.IsTrue(sut.CanLogin());

            IWindowService _fakeWin = ServiceContainer.GetService<IWindowService>();
            Assert.IsTrue(sut.LogInCommand.CanExecute(null));
            sut.LogInCommand.Execute(null);
        }

    }
}
