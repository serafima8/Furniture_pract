using FluentValidation;
using Furniture_LebedevaS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Domain.Validators
{ 
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.User_id).NotEmpty().WithMessage("Id пользователя обязателен");
            RuleFor(order => order.Product_id).NotEmpty().WithMessage("Id продукта обязателен");
            RuleFor(order => order.Date).NotEmpty().WithMessage("Дата обязательна");
        }
    }
}
    