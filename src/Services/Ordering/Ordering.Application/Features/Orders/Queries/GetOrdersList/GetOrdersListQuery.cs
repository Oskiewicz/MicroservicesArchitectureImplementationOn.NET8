using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<List<OrdersVm>>
    {
        public string UserName { get; private set; }
        public GetOrdersListQuery(string userName) 
        {
            this.UserName = userName;
        }
    }
}