using Application.Exceptions;
using Application.Interfaces;
using Application.Product.DTO;
using Application.Product.Validation;
using AutoMapper;

namespace Application.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<ProductDTO>> Get()
        {
            var products = await _repository.GetAll();
            var productsDto = _mapper.Map<List<Domain.Entity.Product>, List<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {

            var product = await _repository.Get(id);
            if (product == null)
                throw new ProductNotFoundException("Материал не найден");
            var productDto = _mapper.Map<Domain.Entity.Product, ProductDTO>(product);
            return productDto;
        }


        public async Task Create(ProductDTO productDto)
        {
            //validation
            var createProductValidation = new CreateProductValidation(productDto);
            createProductValidation.Validate();
            var product = _mapper.Map<ProductDTO, Domain.Entity.Product>(productDto);
            await _repository.Create(product);
        }

        public async Task Update(ProductDTO productDto)
        {

            var updateProductValidation = new UpdateProductValidation(productDto);
            updateProductValidation.Validate();

            bool isProductExist = _repository.IsExist(productDto.Id);
            if (!isProductExist)
                throw new ProductNotFoundException("Материал не найден");

            var product = _mapper.Map<ProductDTO, Domain.Entity.Product>(productDto);

            await _repository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            var product = await _repository.Get(id);
            if (product == null)
                throw new ProductNotFoundException("Материал не найден");
            await _repository.Delete(product);
        }
    }
}
