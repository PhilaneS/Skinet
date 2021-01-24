using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Speficications
{
    public class OrderWithOrderItemsAndOrderingSpecification : BaseSpecificaton<Order>
    {
        public OrderWithOrderItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddIncdude(o => o.OrderItems);
            AddIncdude(o => o.DeliveryMethod);
            AddOrderbyDescending(o => o.OrderDate);
        }

        public OrderWithOrderItemsAndOrderingSpecification(int id, string email) : 
        base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddIncdude(o => o.OrderItems);
            AddIncdude(o => o.DeliveryMethod);
        }
    }
}