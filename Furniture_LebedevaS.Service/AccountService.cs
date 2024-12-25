using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Furniture_LebedevaS.DAL.Interface;
using Furniture_LebedevaS.Domain.ModelsDb;
using Furniture_LebedevaS.Service.Interface;
using Furniture_LebedevaS.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Furniture_LebedevaS.Domain.Enum;
using Furniture_LebedevaS.Domain.Models;
using System.Security.Claims;
using Furniture_LebedevaS.Domain.Helpers;
using Furniture_LebedevaS.Domain.Validators;
using FluentValidation;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Microsoft.Extensions.DependencyInjection;



namespace Furniture_LebedevaS.Service
{
    public class AccountService :  IAccountService
    {
        private readonly IBaseStorage<UserDb> _userStorage;
        private IMapper _mapper { get; set; }


        private UserValidator _validationRules { get; set; }


        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public AccountService(IBaseStorage<UserDb> userStorage)
        {
            _userStorage = userStorage;
            _mapper = mapperConfiguration.CreateMapper();
            _validationRules = new UserValidator();

        }

        public async Task<BaseResponse<string>> Register(User model)
        {
            try
            {
                Random random = new Random();
                string confirmationCode=$"{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";
                await _validationRules.ValidateAndThrowAsync(model);

                var userDb = _mapper.Map<UserDb>(model);

                if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
                {
                    return new BaseResponse<string>()
                    {
                        Description = "Пользователь с такой почтой уже есть",
                    };
                }
               await SendEmail(model.Email, confirmationCode);

                return new BaseResponse<string>()
                {
                    Data = confirmationCode,
                    Description = "Пользователь зарегистрирован",
                    StatusCode = StatusCode.OK,
                };

            }
            catch (ValidationException ex)
            {
                var errorMessages = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));
                return new BaseResponse<string>()
                {
                    Description = errorMessages,
                    StatusCode = StatusCode.BadRequest,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(User model)
        {
            try
            {
                await _validationRules.ValidateAndThrowAsync(model);

                var userDb = _mapper.Map<UserDb>(model);

                userDb = await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);

                if (userDb == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };


                }
                string hashPass = HashPasswordHelper.HashPassword(model.Password);
                if (userDb.Password != hashPass)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или почта"
                    };
                }
                model = _mapper.Map<User>(userDb);
                var result = AuthenticateUserHelper.Authenticate(model);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (ValidationException ex)
            {
                var errorMessages = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = errorMessages,
                    StatusCode = StatusCode.BadRequest,
                };
            }
            catch (Exception ex)
            {
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = ex.Message,
                        StatusCode = StatusCode.InternalServerError
                    };
                }
            }
        }
        public async Task SendEmail(string email, string confirmationCode)
        {
            string path = @"C:\Учебная практика\passwordPractice1.txt";
            var emailMessage = new MimeMessage();


            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "FurnComp@bk.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Добро пожаловать!";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<html>" + "<head>" + "<style>" +
                 "body{font-family:Arial,sans-serif;background-color:#f2f2f2;}" +
                 ".container{max-width:600px;margin:0 auto;padding:20px;background-color:#fff;border-radius:10px; box-shadow:0px 0px 10px rgba(0,0,0,0.1);}" +
                 ".header{text-align:center;margin-bottom:20px;}" +
                 ".message{font-size:16px;line-height:1.6;}" +
                 ".conteiner-code{background-color:#f0f0f0;padding:5px;border-radius:5px;font-weight:bold;}" +
                 ".code {text-align:center;}" +
                 "</style>" +
                 "</head>" +
                 "<body>" +
                 "<div class='container'>" +
                 "<div class='header'><h1>Добро пожаловать на сайт мебели</h1></div>" +
                 "<div class='message'>" +
                 "<p>Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш email и завершить регистрацию:</p>" +
                 "<div class='conteiner-code'><p class='code'>" + confirmationCode + "</p></div>" +
                 "</div>" + "</div>" + "</body>" + "</html>"
            };

           using  var reader = new StreamReader(path);
            
                string password = await reader.ReadToEndAsync();
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("serafima.xd@gmail.com", password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
                throw;
            }

        }

        public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode)
        {
            try
            {
                if (code != confirmCode)
                {
                    throw new Exception("Неверный код! Регистрация не выполнена.");
                }
                model.PathImage = "/images/user.png";
                model.CreateAd = DateTime.Now;
                model.Password = HashPasswordHelper.HashPassword(model.Password);

                await _validationRules.ValidateAndThrowAsync(model);

                var userDb = _mapper.Map<UserDb>(model);

                await _userStorage.Add(userDb);

                var result = AuthenticateUserHelper.Authenticate(model);
                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model)
        {
            try
            {
                var userDb = new UserDb();
                if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
                {

                    model.Password = "google";
                    model.CreateAd = DateTime.Now;

                    userDb = _mapper.Map<UserDb>(model);

                    await _userStorage.Add(userDb);

                    var resultRegister = AuthenticateUserHelper.Authenticate(model);
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = resultRegister,
                        Description = "Объект добавился",
                        StatusCode = StatusCode.OK
                    };
                }

                var resultLogin = AuthenticateUserHelper.Authenticate(model);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = resultLogin,
                    Description = "Объект уже был создан",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
