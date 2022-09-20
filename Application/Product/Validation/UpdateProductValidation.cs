using Application.Product.DTO;

namespace Application.Product.Validation
{
    public class UpdateProductValidation : ProductValidation
    {

        public UpdateProductValidation(ProductDTO productDto) : base(productDto)
        {

        }
        public override void Validate()
        {
            ValidateCount();
        }
    }
}
