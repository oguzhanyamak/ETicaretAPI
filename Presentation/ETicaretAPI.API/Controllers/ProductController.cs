using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAsyncReadRepository _readRepository;
        public ProductController(IProductAsyncReadRepository readrepository)
        {
            _readRepository = readrepository;
        }


        [HttpGet]
        public async Task<IActionResult> getProduct(string Id)
        {
           Product p =  await _readRepository.GetByIdAsync(Id);
           return Ok(p);
        }
    }
}
