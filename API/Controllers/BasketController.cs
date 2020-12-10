using System.Threading.Tasks;
using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketReporsitory;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketReporsitory, IMapper mapper)
        {
            _mapper = mapper;
            _basketReporsitory = basketReporsitory;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketReporsitory.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomBasketDto,CustomerBasket>(basket);

            var updatedBasket = await _basketReporsitory.UpdateBasketAsync(customerBasket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketReporsitory.DeleteBasketAsync(id);
        }
    }
}