using Application.Product.DTO;

namespace Application.Product.Validation
{
    public class CreateProductValidation : ProductValidation
    {
        public CreateProductValidation(ProductDTO productDto) : base(productDto) { }


        public override void Validate()
        {
            ValidateCount();
        }
    }
}
