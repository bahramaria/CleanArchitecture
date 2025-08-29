using CleanArchitecture.Ordering.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Persistence.Repositories;

internal class OrderRepository(OrderingDbContext db) : Domain.Repositories.IOrderRepository
{
    public void Add(Order order)
    {
        db.Set<Order>().Add(order);
    }

    public async Task<bool> Exists(int orderId)
    {
        return await
            db.Set<Order>()
            .AnyAsync(x => x.OrderId == orderId);
    }

    public async Task<Order?> FindOrder(int orderId)
    {
        return await
            db.Set<Order>()
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}
