using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities.OrderAggregate;
using Infrastructure.Data;
using Core.Entities;
using Core.Speficications;

namespace Infrastructure.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unityOfWork;
        public OrderService(IUnitOfWork unityOfWork, IBasketRepository basketRepo)
        {
            _unityOfWork = unityOfWork;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmil, int deliveryMethodId, string basketId, Address shipAddress)
        {
            //Get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            //Get items from the product repo
            var items = new List<OrderItem>();

            foreach (var item in basket.items)
            {
                var productItem = await _unityOfWork.Repository<Product>().GetByAsyId(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            //Get develiry method from the repo
            var deliveryMethod = await _unityOfWork.Repository<DeliveryMethod>().GetByAsyId(deliveryMethodId);

            //cal subbotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //create order
            var order = new Order(items, buyerEmil, shipAddress, deliveryMethod, subtotal);
            _unityOfWork.Repository<Order>().Add(order);

            //save order to db
           var result = await _unityOfWork.Complete();
            
            if(result <=0 ) return null;

            //Delete the basket 
            await _basketRepo.DeleteBasketAsync(basketId);

            //return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unityOfWork.Repository<DeliveryMethod>().ListAllAsynch();
        }

        public async Task<Order> GetOrderByIdAsnc(int id, string buyerEmail)
        {
            var spec = new OrderWithOrderItemsAndOrderingSpecification(id,buyerEmail);

            return await _unityOfWork.Repository<Order>().GetEntityWithSpec(spec); 
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithOrderItemsAndOrderingSpecification(buyerEmail);

            return await _unityOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}