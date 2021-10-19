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
    public class TestOrderService
    {
        private const int OrderId = 1;

        private NTPWebShopDBContext dbContextMock;

        private IQueryable<Order> lstOrders;

        private OrderService systemUnderTest;

        [Test]
        public void GetOrderAsync_WhenInvoked_CallsFindAsync()
        {
            InvokeGetOrderAsync();
            dbContextMock.Orders.Received(1).FindAsync(Arg.Any<int>());
        }

        [Test]
        public void GetOrdersAsync_WhenInvoked_CallsToListAsync()
        {
            InvokeGetOrdersAsync();
            dbContextMock.Orders.Received(1).ToListAsync();
        }

        [Test]
        public void GetOrderAsync_WhenInvoked_ReturnsExpectedId()
        {
            Task<Order> actual = InvokeGetOrderAsync();
            Assert.That(actual.Id, Is.EqualTo(1));
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

            DbSet<Order> mockSet = NSubstituteUtils.CreateMockDbSet(lstOrders);
            dbContextMock = new NTPWebShopDBContext(options) { Orders = mockSet };
        }

        private void CreateSystemUnderTest()
        {
            systemUnderTest = new OrderService(dbContextMock);
        }

        private Task<Order> InvokeGetOrderAsync()
        {
            return systemUnderTest.GetOrderAsync(OrderId);
        }

        private Task<IEnumerable<Order>> InvokeGetOrdersAsync()
        {
            return systemUnderTest.GetOrdersAsync();
        }

        private void SetupOrders()
        {
            lstOrders = new List<Order>
                            {
                                new Order
                                    {
                                        OrderId = 1, OrderDate = DateTime.Now, DeliveryDate = DateTime.Now,
                                        IsDelivered = false, IsExpressDelivery = DateTime.Now, ProductId = 1,
                                        Quantity = 1, TotalWeight = 10
                                    },
                                new Order
                                    {
                                        OrderId = 2, OrderDate = DateTime.Now, DeliveryDate = DateTime.Now,
                                        IsDelivered = false, IsExpressDelivery = DateTime.Now, ProductId = 2,
                                        Quantity = 1, TotalWeight = 10
                                    }
                            }.AsQueryable();
        }
    }
}