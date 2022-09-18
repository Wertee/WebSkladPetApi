using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Product.DTO;

namespace Application.Product.Validation
{
    public abstract class ProductValidation
    {
        private ProductDTO _productDto;

        protected ProductValidation(ProductDTO productDto)
        {
            _productDto = productDto;
        }

        public abstract void Validate();

        protected void ValidateCount()
        {
            if (_productDto.Count <= 0)
            {
                throw new ProductValidationException("Количество не должно быть меньше или равно нулю");
            }
        }
    }
}
