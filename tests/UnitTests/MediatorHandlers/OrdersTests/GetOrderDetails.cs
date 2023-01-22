using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using ArmedMFG.ApplicationCore.Interfaces;
using ArmedMFG.ApplicationCore.Specifications;
using ArmedMFG.Web.Features.OrderDetails;
using Moq;
using Xunit;

namespace ArmedMFG.UnitTests.MediatorHandlers.OrdersTests;

public class GetOrderDetails
{
    private readonly Mock<IReadRepository<Order>> _mockOrderRepository;

    public GetOrderDetails()
    {
        var item = new OrderItem(new CatalogItemOrdered(1, "ProductName", "URI"), 10.00m, 10);
        var address = new Address(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        Order order = new Order("buyerId", address, new List<OrderItem> { item });

        _mockOrderRepository = new Mock<IReadRepository<Order>>();
        _mockOrderRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<OrderWithItemsByIdSpec>(), default))
            .ReturnsAsync(order);
    }

    [Fact]
    public async Task NotBeNullIfOrderExists()
    {
        var request = new ArmedMFG.Web.Features.OrderDetails.GetOrderDetails("SomeUserName", 0);

        var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
    }
}
