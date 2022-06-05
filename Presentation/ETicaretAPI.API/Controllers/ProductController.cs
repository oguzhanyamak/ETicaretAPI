using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IStorageService _storageService;

        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll().Skip(pagination.Size * pagination.Page).Take(pagination.Size);
            return Ok(new { 
                products,
                totalCount,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) 
        {
            return Ok(await _productReadRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new() {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock

            });
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload() 
        {
           var datas = await _storageService.uploadAsync("resource/files",Request.Form.Files);
            //var datas = await _fileService.uploadAsync("product-images",Request.Form.Files);
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                Filename = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.storageName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
