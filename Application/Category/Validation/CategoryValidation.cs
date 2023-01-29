using Application.Exceptions;
using Application.Category.DTO;

namespace Application.Category.Validation
{
    public class CategoryValidation
    {
        private readonly CategoryDTO _categoryDto;

        public CategoryValidation(CategoryDTO categoryDto)
        {
            _categoryDto = categoryDto;
        }

        public void ValidateName()
        {
            if (_categoryDto.Name.Length < 3)
            {
                throw new CategoryValidationException("Длина наименования не должна быть меньше трех");
            }
        }

        public void ValidateProductsConnectedToCurrentCategory()
        {

        }

    }
}
