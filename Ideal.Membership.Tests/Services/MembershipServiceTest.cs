using System;
using System.Collections.Generic;
using System.Linq;
using Ideal.Core.Data;
using Ideal.Identity.Data;
using Ideal.Identity.Model;
using Ideal.Identity.Settings;
using Ideal.Membership.Execptions;
using Ideal.Membership.Services;
using Ideal.Membership.Settings;
using Ideal.Notifications.Services;
using Ideal.Security.Passwords;
using Moq;
using NUnit.Framework;

namespace Ideal.Membership.Tests
{
    [TestFixture]
    public class MembershipServiceTest
    {
        protected Mock<INotificationService> NotificationService = new Mock<INotificationService>();
        protected Mock<IUserRepository> UserRepository = new Mock<IUserRepository>();
        protected Mock<IPasswordService> PasswordPolicy = new Mock<IPasswordService>();
        protected Mock<IAccountSettings> MembershipSettings = new Mock<IAccountSettings>();
        protected Mock<IUnitOfWork> UnitOfWork = new Mock<IUnitOfWork>();

        [SetUp]
        public void ResetMocks()
        {
            NotificationService = new Mock<INotificationService>();
            UserRepository = new Mock<IUserRepository>();
            PasswordPolicy = new Mock<IPasswordService>();
            MembershipSettings = new Mock<IAccountSettings>();
            UnitOfWork = new Mock<IUnitOfWork>();
        }

        protected MembershipService BuildAccountService()
        {
            return new MembershipService(UserRepository.Object,
                NotificationService.Object,
                PasswordPolicy.Object,
                MembershipSettings.Object,
                UnitOfWork.Object);
        }

        [TestFixture]
        public class TheGetAllMethod : MembershipServiceTest
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

                IMembershipService service = BuildAccountService();

                var users = service.GetAll().ToList();

                Assert.AreEqual(0, users.Count);

            }
        }

        [TestFixture]
        public class TheCreateAccountMethod : MembershipServiceTest
        {
            private IMembershipService _membershipService;

            [SetUp]
            public void SetupAccountService()
            {
                _membershipService = BuildAccountService();
            }

            [Test]
            public void ShouldThrowExceptionIfNoTenant()
            {
                Assert.Throws<ArgumentException>(() => _membershipService.CreateAccount(
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
                Assert.Throws<ArgumentException>(() => _membershipService.CreateAccount(
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
                Assert.Throws<ArgumentException>(() => _membershipService.CreateAccount(
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
                Assert.Throws<ArgumentException>(() => _membershipService.CreateAccount(
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
                Assert.Throws<ArgumentException>(() => _membershipService.CreateAccount(
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
                Assert.Throws<ArgumentException>(() => _membershipService.CreateAccount(
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
                Assert.Throws<ArgumentException>(() => _membershipService.CreateAccount(
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
        public class TheGetByEmailMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

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
        public class TheGetByUsernameMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.MultiTenant).Returns(false);
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                UserRepository.Setup(repo => repo.GetAll()).Returns(() => new List<User>()
                {
                    new User()
                    {
                        Tenant = "tenant",
                        Username = "user1",
                        Email = "user1@test.com"
                    },
                    new User()
                    {
                        Tenant = "tenant",
                        Username = "user2",
                        Email = "user2@test.com"
                    },
                    new User()
                    {
                        Tenant = "tenant",
                        Username = "user3",
                        Email = "user3@test.com"
                    }
                }.AsQueryable());

                _userAccountService = BuildAccountService();
            }

            [TestCase("notfound")]
            public void ShouldThrowUserNotFoundException(string username)
            {
                MembershipService service = BuildAccountService();
                var actual = service.GetByUsername(username);
                Assert.IsNull(actual);
            }

            [TestCase("user1")]
            public void ShouldGetUserByUsername(string username)
            {
                User user = _userAccountService.GetByUsername(username);
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
                var actual = _userAccountService.GetByUsername("");
                Assert.IsNull(actual);
            }
        }

        [TestFixture]
        public class TheGetByIdMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

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
                UserRepository.Verify(m => m.GetById(id), Times.Once);
            }
        }

        [TestFixture]
        public class TheGetByVerificationKeyMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>()
                {
                    new User
                    {
                        VerificationKey = "123ABC"
                    }
                }.AsQueryable());
                _userAccountService = BuildAccountService();
            }

            [TestCase("123ABC")]
            public void ShouldReturnUserWithMatchingVerificationKey(string key)
            {
                var actual = _userAccountService.GetByVerificationKey(key);
                Assert.IsNotNull(actual);
                UserRepository.Verify(x => x.GetAll(), Times.Once);
            }
        }

        [TestFixture]
        public class TheUsernameExistsMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>()
                {
                    new User()
                    {
                        Tenant = "tenant",
                        Username = "user1"
                    }
                }.AsQueryable());
                _userAccountService = BuildAccountService();
            }

            [TestCase("user1", true)]
            [TestCase("nouser", false)]
            [TestCase("", false)]
            public void ShouldReturnTrueIfMatchingUsernameFound(string username, bool isMatch)
            {
                var actual = _userAccountService.UsernameExists(username);
                Assert.AreEqual(isMatch,actual);
            }
        }

        [TestFixture]
        public class TheEmailExistsMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>()
                {
                    new User
                    {
                        Tenant = "tenant",
                        Email = "test@test.com"
                    }
                }.AsQueryable());
                _userAccountService = BuildAccountService();
            }

            [TestCase("test@test.com", true)]
            [TestCase("nomatch@test.com", false)]
            public void ReturnsTrueIfMatchingEmailFound(string email, bool expected)
            {
                var actual = _userAccountService.EmailExists(email);
                Assert.AreEqual(expected,actual);
            }
        }

        [TestFixture]
        public class TheVerifyAccountMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>()
                {
                    new User
                    {
                        Tenant = "tenant",
                        Email = "test@test.com"
                    }
                }.AsQueryable());
                _userAccountService = BuildAccountService();
            }
        }

        [TestFixture]
        public class TheCancelNewAccountMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>()
                {
                    new User
                    {
                        Tenant = "tenant",
                        Email = "test@test.com"
                    }
                }.AsQueryable());
                _userAccountService = BuildAccountService();
            }
        }

        [TestFixture]
        public class TheDeleteAccountMethod : MembershipServiceTest
        {
            private MembershipService _userAccountService;

            [SetUp]
            public void SetupAccountService()
            {
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>()
                {
                    new User
                    {
                        Tenant = "tenant",
                        Email = "test@test.com",
                        Username = "todelete"
                    }
                }.AsQueryable());
                _userAccountService = BuildAccountService();
            }

            [TestCase("todelete",true)]
            [TestCase("noexist",false)]
            public void ShouldDeleteAccount(string username, bool expected)
            {
                MembershipSettings.SetupGet(x => x.AllowAccountDeletion).Returns(true);
                var actual = _userAccountService.DeleteAccount(username);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void CloseAccountInsteadOfDeleteIfSpecified()
            {
                MembershipSettings.SetupGet(x => x.AllowAccountDeletion).Returns(false);
                var deleted = _userAccountService.DeleteAccount("todelete");

                Assert.IsTrue(deleted);
            }
        }

        [TestFixture]
        public class TheAuthenticateMethod : MembershipServiceTest
        {
            [SetUp]
            public void SetupMocks()
            {
                MembershipSettings.SetupGet(x => x.PasswordHashingIterationCount).Returns(5);
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>
                {
                    new User()
                    {
                        Username = "validuser",
                    }
                }.AsQueryable());
            }

            [TestCase("notfound","password")]
            public void ShouldThrowExceptionIfUserNotFound(string username, string password)
            {
                MembershipSettings.SetupGet(x => x.DefaultTenant).Returns("tenant");
                var service = BuildAccountService();
                Assert.Throws<UserNotFoundException>(()=>service.Authenticate(username, password));

                UserRepository.Verify(x=>x.GetAll(), Times.Once);
            }

            [TestCase("validuser","securePassword",true)]
            public void ShouldReturnTrueIfValidUser(string username, string password, bool valid)
            {
                var hashedPassword = CryptoHelper.HashPassword(password,5);

                UserRepository.Setup(x => x.GetAll()).Returns(new List<User>
                {
                    new User()
                    {
                        Username = "validuser",
                        HashedPassword = hashedPassword
                    }
                }.AsQueryable());

                var service = BuildAccountService();
                var actual = service.Authenticate(username, password);

                Assert.AreEqual(actual.IsValid,valid);
            }
        }
    }
}
