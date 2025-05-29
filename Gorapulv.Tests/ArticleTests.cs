// Gorapulv.Tests/ArticleTests.cs
using Gorapulv.Api.Data;
using Gorapulv.Api.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using NUnit.Framework.Legacy;

namespace Gorapulv.Tests
{
    public class ArticleTests
    {
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            _context = new AppDbContext(options);
        }

        [TearDown]
        public void Teardown()
        {
// Nettoyer la base en mémoire entre tests
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void CreateArticle_IncrementsCount()
        {
// Arrange
            var article = new Article
            {
                Title = "Titre test", Content =
                    "Contenu test",
                Author = "Testeur"
            };
// Act
            _context.Articles.Add(article);
            _context.SaveChanges();
// Assert
            int count = _context.Articles.Count();
            Assert.That(count, Is.EqualTo(1));
            Assert.That(article.Id, Is.EqualTo(1));
        }

        [Test]
        public void GetArticle_ById_ReturnsCorrectItem()
        {
// Arrange
            var article = new Article { Title = "Titre 2", Content = "Secondcontenu", Author = "Testeur" };
                _context.Articles.Add(article);
                _context.SaveChanges();

                var found = _context.Articles.Find(article.Id);

                Assert.That(found, Is.Not.Null);
                Assert.That(found!.Title, Is.EqualTo("Titre 2"));
        }
    }
}