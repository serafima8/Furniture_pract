using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Furniture_LebedevaS.Domain;
using Furniture_LebedevaS.Service.Interface;
using Furniture_LebedevaS.Service;
using AutoMapper;
using Furniture_LebedevaS.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Furniture_LebedevaS.Service.LoginAndRegistration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;
using Furniture_LebedevaS.Domain.Enum;
using Furniture_LebedevaS.DAL.Interface;
using Furniture_LebedevaS.Domain.ViewModels.Categories;



namespace Furniture_LebedevaS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IAccountService _accountService { get; set; }
        public ICategoryService _categoryService { get; set; }

        private IMapper _mapper { get; set; }
        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        private readonly IWebHostEnvironment _appEnvironment;
        public HomeController(ILogger<HomeController> logger,IAccountService account, IWebHostEnvironment appEnvironment, ICategoryService categoryService)
        {
            _accountService = account;
            _logger = logger;
            _mapper = mapperConfiguration.CreateMapper();
            _appEnvironment = appEnvironment;
            _appEnvironment = appEnvironment;
            _categoryService = categoryService;
        }

        // Главная страница
        public IActionResult SiteInformation()
        {
            var response = _categoryService.GetAllCategories();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                CategoryViewModel categoryView = new CategoryViewModel() { Categories = response.Data };
                return View(categoryView);
            }
            return View();

        }

        // Вкладка "Контакты"
        public IActionResult Contacts()
        {
            return View();
        }
        // Вкладка "О нас"
        public IActionResult About()
        {
            return View();
        }

        // Вкладка "Услуги"
        public IActionResult Services()
        {
            return View();
        }

        // Вкладка "Страны"
        public IActionResult Countries()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                var response = await _accountService.Login(user);
                if(response.StatusCode==Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(response.Data));
                     return Ok(model);
                }
                ModelState.TryAddModelError("", response.Description);
               
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            return BadRequest(errors);
        }
       

        [HttpPost]
        public async Task <IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                var confirm=_mapper.Map<ConfirmEmailViewModel>(model);
                var code = await _accountService.Register(user);
                confirm.GeneratedCode = code.Data;
                
                return Ok(confirm);
            }
            var errors=ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errors);
        }


        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SiteInformation", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel confirmEmailModel)
        {
            var user=_mapper.Map<User>(confirmEmailModel);
            var response = await _accountService.ConfirmEmail(user, confirmEmailModel.GeneratedCode, confirmEmailModel.CodeConfirm);
            if(response.StatusCode==Domain.Enum.StatusCode.OK)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                return Ok(confirmEmailModel);
            }
            ModelState.AddModelError("", response.Description);
            var errors=ModelState.Values.SelectMany(v=>v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errors);
        }
        public async Task AuthenticationGoogle(string returnUrl="/")
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse", new { returnUrl }),
                    Parameters = { { "prompt", "select_account" } }
                });

        }
        public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (result?.Succeeded == true)
                {
                    User model = new User
                    {
                        Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                        Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                        PathImage = "/" + (await SaveImageInImageUser(result.Principal.FindFirst("picture")?.Value ?? "/Images/default-avatar.jpg", result)) ?? "/Images/designer.jpg"

                    };
                    var response = await _accountService.IsCreatedAccount(model);

                    if (response.StatusCode == Furniture_LebedevaS.Domain.Enum.StatusCode.OK)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.Data));
                        return Redirect(returnUrl);
                    }
                }

                return BadRequest("Аутентификация не удалась");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка в GoogleResponse");
                return StatusCode(500, new { message = e.Message });
            }
        }

        private async Task<string> SaveImageInImageUser(string imageUrl, AuthenticateResult result)
        {
            string filePath = "";
            if (!string.IsNullOrEmpty(imageUrl))
            {
                using (var httpClient = new HttpClient())
                {
                    filePath = Path.Combine("ImageUser", $"{result.Principal?.FindFirst(ClaimTypes.Email)?.Value ?? "default-email"}-avatar.jpg");

                    var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                    await System.IO.File.WriteAllBytesAsync(Path.Combine(_appEnvironment.WebRootPath, filePath), imageBytes);
                }
            }

            return filePath;
        }


    }
}