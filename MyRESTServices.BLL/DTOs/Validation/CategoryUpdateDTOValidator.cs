﻿using FluentValidation;

namespace MyRESTServices.BLL.DTOs.Validation
{
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category Name harus diisi");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Category ID harus diisi");

        }
    }
}
