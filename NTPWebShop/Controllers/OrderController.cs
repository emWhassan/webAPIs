using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTPWebShop.API.Model;
using NTPWebShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NTPWebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _iOrerService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService iOrerService, IMapper mapper)
        {
            _iOrerService = iOrerService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {

            try
            {
                var orders = await _iOrerService.GetOrdersAsync();
                return Ok(orders);
            }
            catch (Exception exp)
            {

                return StatusCode(500, "Internal Server Error! " + exp.Message);
            }

        }


        [HttpPost("Create")]
        public async Task<ActionResult<bool>> Create([FromBody] OrderDto OrderForCreation)
        {
            try
            {
                
                var orderToAdd = _mapper.Map<Domain.Order>(OrderForCreation);
                _iOrerService.AddOrder(orderToAdd);
                await _iOrerService.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception exp)
            {
                return StatusCode(500, "Internal Server Error! " + exp.Message);
            }
        }


        [HttpGet("GetOrer/{Id}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrer(int OrderId)
        {
            try
            {
                if (!await _iOrerService.OrderExistsAsync(OrderId))
                {
                    return NotFound();
                }

                var Product = await _iOrerService.GetOrderAsync(OrderId);
                return Ok(Product);
            }
            catch (Exception exp)
            {

                return StatusCode(500, "Internal Server Error! " + exp.Message);
            }

        }



    }
}
