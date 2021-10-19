using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTPWebShop.API.Constants;
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
    public class ProductController : ControllerBase
    {

        private readonly IProductService _iproductService;
        private readonly IMapper _mapper;


        public ProductController(IProductService iproductService, IMapper mapper)
        {
            _iproductService = iproductService;
            _mapper = mapper;


        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {

            try
            {
                var Products = await _iproductService.GetProductsAsync();
                IEnumerable<ProductDto> productDto = _mapper.Map<IEnumerable<ProductDto>>(Products);
                foreach (var item in productDto)
                {
                    if (item.Unit>1 && item.Unit<=10)
                    {
                        item.deliveryDate = DateTime.Today.AddDays(EstimatedDeliveryDays.Days_3).ToString("dd-MM-yyyy");
                    }
                    if (item.Unit > 10 && item.Unit <= 20)
                    {
                        item.deliveryDate = DateTime.Today.AddDays(5).ToString("dd-MM-yyyy");
                    }
                    if (item.Unit > 20 && item.Unit <= 100)
                    {
                        item.deliveryDate = DateTime.Today.AddDays(10).ToString("dd-MM-yyyy");
                    }
                }
                return Ok(productDto);
            }
            catch (Exception exp)
            {

                return StatusCode(500, "Internal Server Error! " + exp.Message);
            }
           
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> Create([FromBody] ProductDto ProductForCreation)
        {

            try
            {
                var productToAdd = _mapper.Map<Domain.Product>(ProductForCreation);
                productToAdd.CreatedOn = DateTime.Now;
                productToAdd.IsActive = true;
                _iproductService.AddProduct(productToAdd);
                await _iproductService.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception exp)
            {
                return StatusCode(500,"Internal Server Error! "+ exp.Message);
            }
           
        }




        [HttpPut("Update/")]
        public async Task<IActionResult> Update(ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                var productObj = await _iproductService.GetProductAsync(product.ProductId.Value);
                if (productObj == null)
                {
                    return NotFound();
                }
                if (true)
                {
                    product.CreatedOn = productObj.CreatedOn;
                    product.IsActive = productObj.IsActive;
                }
               
               var productToUpdate = _mapper.Map<Domain.Product>(product);
                productToUpdate.UpdatedOn = DateTime.Now;
                _iproductService.UpdateProduct(product.ProductId.Value,productToUpdate);
                return Ok();
            }
            catch (Exception exp)
            {

                return StatusCode(500, "Internal Server Error! " + exp.Message);
            }
        }



        [HttpGet("{productId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct(int? productId)
        {
            try
            {
                if (!await _iproductService.ProductExistsAsync(productId))
                {
                    return NotFound();
                }

                var Product = await _iproductService.GetProductAsync(productId);
                return Ok(Product);
            }
            catch (Exception exp)
            {
                return StatusCode(500, "Internal Server Error! " + exp.Message);
            }
            
        }

       
    }
}
