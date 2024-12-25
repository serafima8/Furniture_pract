using FluentValidation;
using Furniture_LebedevaS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Domain.Validators
{
    public class CategoriesValidator : AbstractValidator<Category>
    {
        public CategoriesValidator() 
        {
            RuleFor(categories => categories.Name).NotEmpty().WithMessage("Название категории обязательно");
            RuleFor(categories => categories.Description).NotEmpty().WithMessage("Описание категории обязательно");
        }
    }
}
