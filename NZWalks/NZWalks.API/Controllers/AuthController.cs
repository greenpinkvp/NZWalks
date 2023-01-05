using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.Users;
using NZWalks.API.Repositories.IRepository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;

        public AuthController(IUnitOfWork unitOfWork, ITokenHandler tokenHandler, IMapper mapper)
        {
            _tokenHandler = tokenHandler;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            //check if user authenticated
            //check username and password
            var user = await _unitOfWork.User.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
            if (user != null)
            {
                //generate a token
                var token = await _tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return Unauthorized("Username or password is incorrect.");
        }
    }
}