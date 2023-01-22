using System.Collections.Generic;
using ArmedMFG.Web.ViewModels;
using MediatR;

namespace ArmedMFG.Web.Features.MyOrders;

public class GetMyOrders : IRequest<IEnumerable<OrderViewModel>>
{
    public string UserName { get; set; }

    public GetMyOrders(string userName)
    {
        UserName = userName;
    }
}
