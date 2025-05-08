using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkufMusic.Core.Services;
using SkufMusic.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using SkufMusic.Core.Services.UserServices;
using SkufMusic.Data.Data;

namespace SkufMusic.Test.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private MusicStoreDbContext _context;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MusicStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new MusicStoreDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _userService = new UserService(_context);
        }

        [TestMethod]
        public async Task RegisterAsync_CreatesNewUser()
        {
            var user = await _userService.RegisterAsync("testuser", "password123");

            Assert.IsNotNull(user);
            Assert.AreEqual("testuser", user.Username);
            Assert.IsTrue(await _userService.UserExistsAsync("testuser"));
        }

        [TestMethod]
        public async Task AuthenticateAsync_ReturnsUser_WhenCredentialsCorrect()
        {
            await _userService.RegisterAsync("user2", "pass2");

            var authenticated = await _userService.AuthenticateAsync("user2", "pass2");

            Assert.IsNotNull(authenticated);
            Assert.AreEqual("user2", authenticated.Username);
        }

        [TestMethod]
        public async Task AuthenticateAsync_ReturnsNull_WhenPasswordIncorrect()
        {
            await _userService.RegisterAsync("user3", "pass3");

            var result = await _userService.AuthenticateAsync("user3", "wrongpass");

            Assert.IsNull(result);
        }
    }
}
