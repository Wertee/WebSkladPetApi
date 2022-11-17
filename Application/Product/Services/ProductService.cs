using Application.Exceptions;
using Application.Interfaces;
using Application.Product.DTO;
using Application.Product.Validation;
using AutoMapper;

namespace Application.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ProductDTO>> Get()
        {
            //var products = await _repository.GetAllAsync();
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            var productsDto = _mapper.Map<List<Domain.Entity.Product>, List<ProductDTO>>(products);
            return productsDto;
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {

            //var product = await _repository.GetByIdAsync(id);
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
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
            //await _repository.CreateAsync(product);
            await _unitOfWork.ProductRepository.CreateAsync(product);
        }

        public async Task Update(ProductDTO productDto)
        {

            var updateProductValidation = new UpdateProductValidation(productDto);
            updateProductValidation.Validate();

            //bool isProductExist = _repository.IsExist(productDto.Id);
            bool isProductExist = _unitOfWork.ProductRepository.IsExist(productDto.Id);
            if (!isProductExist)
                throw new ProductNotFoundException("Материал не найден");

            var product = _mapper.Map<ProductDTO, Domain.Entity.Product>(productDto);

            //await _repository.UpdateAsync(product);
            await _unitOfWork.ProductRepository.UpdateAsync(product);
        }

        public async Task Delete(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new ProductNotFoundException("Материал не найден");
            await _repository.DeleteAsync(product);
        }
    }
}
