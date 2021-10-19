namespace NTPWebShop.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using NSubstitute;

    using NTPWebShop.Data;
    using NTPWebShop.Domain;
    using NTPWebShop.Services.Concrete;

    using NUnit.Framework;

    [TestFixture]
    public class TestProductService
    {
        private const int ProductId = 2;

        private NTPWebShopDBContext dbContextMock;

        private IQueryable<Product> lstOrders;

        private ProductService systemUnderTest;

        [Test]
        public void GetOrderAsync_WhenInvoked_CallsFindAsync()
        {
            InvokeGetProductAsync();
            dbContextMock.Products.Received(1).FindAsync(Arg.Any<int>());
        }

        [Test]
        public void GetOrderAsync_WhenInvoked_CallsToListAsync()
        {
            InvokeGetProductsAsync();
            dbContextMock.Products.Received(1).ToListAsync();
        }

        [Test]
        public void GetOrderAsync_WhenInvoked_ReturnsExpectedId()
        {
            Task<Product> actual = InvokeGetProductAsync();
            Assert.That(actual.Id, Is.EqualTo(2));
        }

        [SetUp]
        public void Setup()
        {
            SetupOrders();
            CreateMock();
            CreateSystemUnderTest();
        }

        private void CreateMock()
        {
            DbContextOptions<NTPWebShopDBContext> options = new DbContextOptionsBuilder<NTPWebShopDBContext>()
                .UseSqlServer("fakeDb").Options;

            DbSet<Product> mockSet = NSubstituteUtils.CreateMockDbSet(lstOrders);
            dbContextMock = new NTPWebShopDBContext(options) { Products = mockSet };
        }

        private void CreateSystemUnderTest()
        {
            systemUnderTest = new ProductService(dbContextMock);
        }

        private Task<Product> InvokeGetProductAsync()
        {
            return systemUnderTest.GetProductAsync(ProductId);
        }

        private Task<IEnumerable<Product>> InvokeGetProductsAsync()
        {
            return systemUnderTest.GetProductsAsync();
        }

        private void SetupOrders()
        {
            lstOrders = new List<Product>
                            {
                                new Product
                                    {
                                        ProductId = 1, Name = "Product1", Unit = 1, CreatedOn = DateTime.Now,
                                        InStock = 1, IsActive = false, UpdatedOn = DateTime.Now
                                    },
                                new Product
                                    {
                                        ProductId = 2, Name = "Product1", Unit = 1, CreatedOn = DateTime.Now,
                                        InStock = 1, IsActive = false, UpdatedOn = DateTime.Now
                                    }
                            }.AsQueryable();
        }
    }
}