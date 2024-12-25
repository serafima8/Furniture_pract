﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Domain
{
    public class RegisterViewModel
    {
        
        [Required(ErrorMessage ="Укажите логин 3-20 символов")]
        [MaxLength(20,ErrorMessage ="Логин должен иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должен иметь длину более 3 символов")]
        public string Login { get; set; }
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        [Required(ErrorMessage = "Укажите почту")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Укажите пароль")]
        [MinLength(6,ErrorMessage ="Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Подтвердите пароль")]
        [Compare("Password", ErrorMessage ="Пароли не совпадают")]
        public string PasswordReset { get; set; }
    }
}
