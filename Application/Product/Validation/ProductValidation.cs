using Application.Exceptions;

namespace Application.Product.Validation
{
    public class ProductValidation
    {
        private readonly Domain.Entity.Product _product;

        public ProductValidation(Domain.Entity.Product product)
        {
            _product = product;
        }

        public void ValidateCount()
        {
            if (_product.Count <= 0)
            {
                throw new ProductValidationException("Количество должно быть больше нуля");
            }
        }

    }
}
