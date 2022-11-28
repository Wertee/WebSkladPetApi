using Application.Product.DTO;

namespace Application.Product.Validation
{
    public class UpdateProductValidation : ProductValidation
    {

        public UpdateProductValidation(Domain.Entity.Product product) : base(product) { }

    }
}
