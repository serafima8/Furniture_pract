using FluentValidation;
using Furniture_LebedevaS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Название обязательно");
            RuleFor(product => product.Description).NotEmpty().WithMessage("Описание обязательно");
            RuleFor(product => product.Price).GreaterThan(0).WithMessage("Цена должна быть больше нуля");
        }
    }
}
