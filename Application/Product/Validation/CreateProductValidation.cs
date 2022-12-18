using Application.Product.DTO;

namespace Application.Product.Validation
{
    public class CreateProductValidation : ProductValidation
    {
        public CreateProductValidation(Domain.Entity.Product product) : base(product) { }

    }
}
