using Application.Exceptions;

namespace Application.Product.Validation
{
    public abstract class ProductValidation
    {
        private readonly Domain.Entity.Product _product;

        protected ProductValidation(Domain.Entity.Product product)
        {
            _product = product;
        }

        public void ValidateCount()
        {
            if (_product.Count <= 0)
            {
                throw new ProductValidationException("Количество не должно быть меньше или равно нулю");
            }
        }

    }
}
