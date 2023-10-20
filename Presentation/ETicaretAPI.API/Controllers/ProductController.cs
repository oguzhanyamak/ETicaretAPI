using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetByName;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediator _mediator;
        public ProductController(IWebHostEnvironment webHostEnvironment, IMediator mediator)
        {

            _webHostEnvironment = webHostEnvironment;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllProductQueryRequest request = new();
            GetAllProductQueryResponse response = await _mediator.Send(request);
            return Ok(response.Products);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{ProdName}")]
        public async Task<IActionResult> GetByName([FromRoute]GetProductsByNameRequest request)
        {
            GetProductsByNameResponse res = await _mediator.Send(request);
            return Ok(res.Products);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource\\productImages");
            if(!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            foreach(IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath,$"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}" );
                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,1024*1024,useAsync:false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }
    }
}
