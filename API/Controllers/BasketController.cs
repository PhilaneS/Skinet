using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketReporsitory;
        public BasketController(IBasketRepository basketReporsitory)
        {
            _basketReporsitory = basketReporsitory;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id){
            var basket = await _basketReporsitory.GetBasketAsync(id);
            return Ok(basket?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket){
            var updatedBasket = await _basketReporsitory.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id){
            await _basketReporsitory.DeleteBasketAsync(id);
        }
    }
}