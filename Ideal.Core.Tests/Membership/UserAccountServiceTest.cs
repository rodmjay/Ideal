using System;
using System.Collections.Generic;
using System.Linq;
using Ideal.Core.Interfaces.Data;
using Ideal.Core.Interfaces.Membership;
using Ideal.Core.Interfaces.Notifications;
using Ideal.Core.Model;
using Ideal.Core.Services;
using Moq;
using NUnit.Framework;

namespace Ideal.Core.Tests.Membership
{
    [TestFixture]
    public class UserAccountServiceTest
    {
        protected Mock<INotificationService> NotificationService = new Mock<INotificationService>();
        protected Mock<IUserRepository> UserRepository = new Mock<IUserRepository>();
        protected Mock<IPasswordPolicy> PasswordPolicy = new Mock<IPasswordPolicy>();
        protected Mock<IMembershipSettings> MembershipSettings = new Mock<IMembershipSettings>();
        protected Mock<IUnitOfWork> UnitOfWork = new Mock<IUnitOfWork>();

        protected UserAccountService BuildAccountService()
        {
            return new UserAccountService(UserRepository.Object,
                NotificationService.Object,
                PasswordPolicy.Object,
                MembershipSettings.Object,
                UnitOfWork.Object);
        }

        [TestFixture]
        public class TheGetAllMethod : UserAccountServiceTest
        {
            [Test]
            public void NoUsersReturnedIfNoDefaultTenant()
            {
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>()
                {
                    new User(),
                    new User(),
                    new User()
                }.AsQueryable());

                UserAccountService service = BuildAccountService();

                var users = service.GetAll().ToList();

                Assert.AreEqual(0,users.Count);

            }
        }

        [TestFixture]
        public class TheCreateAccountMethod : UserAccountServiceTest
        {
            private UserAccountService _accountService;

            [SetUp]
            public void SetupAccountService()
            {
                _accountService = BuildAccountService();
            }

            [Test]
            public void ShouldThrowExceptionIfNoTenant()
            {
                Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(
                    "", // tenant
                    "rodmjay",
                    "12345",
                    "rodmjay@gmail.com",
                    "rod",
                    "johnson",
                    "123-123-1234",
                    "123 Main Street"));
            }

            [Test]
            public void ShouldThrowExceptionIfNoUsername()
            {
                Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(
                    "tenant",
                    "", // username
                    "12345",
                    "rodmjay@gmail.com",
                    "rod",
                    "johnson",
                    "123-123-1234",
                    "123 Main Street"));
            }

            [Test]
            public void ShouldThrowExceptionIfNoPassword()
            {
                Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(
                    "tenant", 
                    "rodmjay",
                    "", // password
                    "rodmjay@gmail.com",
                    "rod",
                    "johnson",
                    "123-123-1234",
                    "123 Main Street"));
            }

            [Test]
            public void ShouldThrowExceptionIfNoEmail()
            {
                Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(
                    "tenant",
                    "rodmjay",
                    "12345",
                    "", // email
                    "rod",
                    "johnson",
                    "123-123-1234",
                    "123 Main Street"));
            }

            [Test]
            public void ShouldThrowExceptionIfNoFirstName()
            {
                Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(
                    "tenant",
                    "rodmjay",
                    "12345",
                    "rodmjay@gmail.com",
                    "",
                    "johnson",
                    "123-123-1234",
                    "123 Main Street"));
            }

            [Test]
            public void ShouldThrowExceptionIfNoLastName()
            {
                Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(
                    "tenant",
                    "rodmjay",
                    "12345",
                    "rodmjay@gmail.com",
                    "rod",
                    "",
                    "123-123-1234",
                    "123 Main Street"));
            }

            [Test]
            public void ShouldThrowExceptionIfNoPhone()
            {
                Assert.Throws<ArgumentException>(() => _accountService.CreateAccount(
                    "tenant",
                    "rodmjay",
                    "12345",
                    "rodmjay@gmail.com",
                    "rod",
                    "johnson",
                    "",
                    "123 Main Street"));
            }
        }

        [TestFixture]
        public class TheGetByEmailMethod : UserAccountServiceTest
        {
            private UserAccountService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.MultiTenant).Returns(true);
                UserRepository.Setup(repo => repo.GetAll()).Returns(() => new List<User>()
                {
                    new User()
                    {
                        Tenant = "tenant1",
                        Username = "user1",
                        Email = "user1@test.com"
                    },
                    new User()
                    {
                        Tenant = "tenant1",
                        Username = "user2",
                        Email = "user2@test.com"
                    },
                    new User()
                    {
                        Tenant = "tenant2",
                        Username = "user3",
                        Email = "user3@test.com"
                    }
                }.AsQueryable());
                _userAccountService = BuildAccountService();
            }

            [TestCase("tenant1", "user1@test.com")]
            public void ShouldGetUserByEmail(string tenant, string email)
            {
                User user = _userAccountService.GetByEmail(tenant, email);
                Assert.IsNotNull(user);
                Assert.AreEqual(user.Email, email);
            }
        }

        [TestFixture]
        public class TheGetByUsernameMethod : UserAccountServiceTest
        {
            private UserAccountService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.MultiTenant).Returns(true);

                UserRepository.Setup(repo => repo.GetAll()).Returns(() => new List<User>()
                {
                    new User()
                    {
                        Tenant = "tenant1",
                        Username = "user1",
                        Email = "user1@test.com"
                    },
                    new User()
                    {
                        Tenant = "tenant1",
                        Username = "user2",
                        Email = "user2@test.com"
                    },
                    new User()
                    {
                        Tenant = "tenant2",
                        Username = "user3",
                        Email = "user3@test.com"
                    }
                }.AsQueryable());

                _userAccountService = BuildAccountService();
            }

            [TestCase("tenant1","user1")]
            public void ShouldGetUserByUsername(string tenant, string username)
            {
                User user = _userAccountService.GetByUsername(tenant, username);
                Assert.IsNotNull(user);
                Assert.AreEqual(user.Username, username);
            }

            [Test]
            public void ShouldThrowExceptionIfNoTenant()
            {
                var actual = _userAccountService.GetByUsername("", "user");
                Assert.IsNull(actual);
            }

            [Test]
            public void ShouldThrowExceptionIfNoUsername()
            {
                var actual = _userAccountService.GetByUsername("tenant", "");
                Assert.IsNull(actual);
            }
        }

        [TestFixture]
        public class TheGetByIdMethod : UserAccountServiceTest
        {
            private UserAccountService _userAccountService;
          
            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.MultiTenant).Returns(true);
                _userAccountService = BuildAccountService();
            }

            [TestCase(1)]
            [TestCase(2)]
            [TestCase(3)]
            public void ShouldReturnUserWithMatchingId(int id)
            {
                _userAccountService.GetByID(id);
                UserRepository.Verify(m=>m.GetById(id), Times.Once);
            }
        }
    }
}
