using Application.Exceptions;
using Application.Interfaces;
using Application.Product.DTO;
using Application.Product.Validation;
using AutoMapper;

namespace Application.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }
        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _uof.ProductRepository.GetAllAsync();
            var productsDto = _mapper.Map<List<Domain.Entity.Product>, List<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            var product = await _uof.ProductRepository.GetByIdAsync(id);
            if (product == null)
                throw new ProductNotFoundException("Материал не найден");
            var productDto = _mapper.Map<Domain.Entity.Product, ProductDTO>(product);
            return productDto;
        }


        public async Task CreateAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<ProductDTO, Domain.Entity.Product>(productDto);
            //validation
            var createProductValidation = new ProductValidation(product);
            createProductValidation.ValidateCount();

            _uof.ProductRepository.Create(product);
            await _uof.SaveAsync();
        }

        public async Task UpdateAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<ProductDTO, Domain.Entity.Product>(productDto);

            bool isProductExist = _uof.ProductRepository.IsExist(productDto.Id);
            if (!isProductExist)
                throw new ProductNotFoundException("Материал не найден");
            var updateProductValidation = new ProductValidation(product);
            updateProductValidation.ValidateCount();

            _uof.ProductRepository.Update(product);
            await _uof.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _uof.ProductRepository.GetByIdAsync(id);
            if (product == null)
                throw new ProductNotFoundException("Материал не найден");
            _uof.ProductRepository.Delete(product);
            await _uof.SaveAsync();
        }
    }
}
