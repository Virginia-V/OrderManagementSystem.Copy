using AutoMapper;
using OMS.Bll.Interfaces;
using OMS.Common;
using OMS.Common.Dtos.Products;
using OMS.Common.Exceptions;
using OMS.Common.Models.PagedRequest;
using OMS.Dal.Interfaces;
using OMS.Domain;

namespace OMS.Bll.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var records = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(records);
        }

        public async Task<UpdateProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var result = _mapper.Map<UpdateProductDto>(product);
            return result;
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            var products = await _productRepository.GetWhereAsync(p => p.ProductSKU == dto.ProductSKU);
            var productWithSameSku = products.FirstOrDefault();
            if (productWithSameSku != null)
            {
                throw new ValidationException(ErrorMessages.ProductAlreadyExists);
            }
            var product = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ValidationException(ErrorMessages.NoProductForUpdate);
            }
            if (!string.IsNullOrWhiteSpace(dto.ProductName))
                product.ProductName = dto.ProductName;

            if (dto.CategoryId.HasValue)
                product.CategoryId = dto.CategoryId.Value;

            if (dto.ProductPrice.HasValue)
                product.ProductPrice = dto.ProductPrice.Value;

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ValidationException(ErrorMessages.NoProductForDelete);
            }
            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductQuantityDto>> GetTopFiveSellingProductsAsync()
        {
            var records = await _productRepository.GetTopFiveSellingProductsAsync();
            return records;
        }

        public async Task<PaginatedResult<ProductDto>> GetPagedProductsAsync(PagedRequest pagedRequest)
        {
            var dbItems = await _productRepository.GetPagedDataAsync(pagedRequest);
            return new PaginatedResult<ProductDto>
            {
                Items = _mapper.Map<IEnumerable<ProductDto>>(dbItems.Value).ToList(),
                PageIndex = pagedRequest.PageIndex,
                PageSize = pagedRequest.PageSize,
                Total = dbItems.Key
            };
        }
    }
}
