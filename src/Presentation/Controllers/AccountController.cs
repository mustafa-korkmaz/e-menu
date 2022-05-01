using Application.Constants;
using Application.Services.Account;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels.User;
using System.Net;
using Application.Dto.User;
using Application.Services.Tenant;

namespace Presentation.Controllers
{
    [ApiController, Authorize(AppConstants.DefaultAuthorizationPolicy)]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var userDto = await _accountService.GetUserAsync();

            var viewModel = _mapper.Map<UserViewModel>(userDto);

            return Ok(viewModel);
        }

        [ModelStateValidation]
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.Created)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AddUserViewModel model)
        {
            var userDto = _mapper.Map<UserDto>(model);

            await _accountService.RegisterAsync(userDto, model.Password!);

            var viewModel = _mapper.Map<UserViewModel>(userDto);

            return Created($"users/{viewModel.Id}", viewModel);
        }

        [ModelStateValidation]
        [HttpPost("token")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody] GetTokenViewModel model)
        {
            var userDto = new UserDto
            {
                Email = model.EmailOrUsername!.GetNormalized(),
                Username = model.EmailOrUsername!.GetNormalized()
            };

            var token = await _accountService.GetTokenAsync(userDto, model.Password!);

            var resp = _mapper.Map<TokenViewModel>(userDto);

            resp.Token = token;

            return Ok(resp);
        }
    }
}