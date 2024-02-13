using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Flight.DTO;
using Flight.Services;
using Flight.Entity;
using Flight.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Flight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
            private readonly IUserService userService;
            private readonly IMapper _mapper;
            private readonly IConfiguration configuration;
            

            public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)

            {
                this.userService = userService;
                _mapper = mapper;
                this.configuration = configuration;


            }
            [HttpGet, Route("GetAllUsers")]
            //[AllowAnonymous]
            public IActionResult GetAllUsers()
            {
                try
                {
                    List<User> users = userService.GetAllUsers();
                    List<UserDTO> usersDto = _mapper.Map<List<UserDTO>>(users);
                    return StatusCode(200, users);

                }
                catch (Exception ex)
                {

                    return StatusCode(500, ex.Message);
                }
            }
            [HttpPost, Route("Register")]
            //[AllowAnonymous] //access the endpoint any any user with out login
            public IActionResult AddUser(UserDTO userDto)
            {
                try
                {
                    User user = _mapper.Map<User>(userDto);
                    userService.CreateUser(user);
                    return StatusCode(200, user);
                    // return Ok(); //return emplty result

                }
                catch (Exception ex)
                {

                    return StatusCode(500, ex.InnerException.Message);

                }
            }
            //PUT /EditUser
            [HttpPut, Route("EditUser")]
            //[AllowAnonymous]
            public IActionResult EditUser(UserDTO userDto)
            {
                try
                {
                    User user = _mapper.Map<User>(userDto);
                    userService.EditUser(user);
                    return StatusCode(200, user);
                    // return Ok(); //return emplty result

                }
                catch (Exception ex)
                {

                    return StatusCode(500, ex.InnerException.Message);
                }
            }
            [HttpGet, Route("GetUserById/{userId}")]
            //[AllowAnonymous]
            public IActionResult GetUserById(int userId)
            {
                try
                {
                    User user = userService.GetUserById(userId);

                    if (user == null)
                    {
                        return NotFound($"User with ID {userId} not found");
                    }

                    UserDTO userDto = _mapper.Map<UserDTO>(user);
                    return StatusCode(200, userDto);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, ex.Message);
                }
            }
            [HttpPost, Route("Validate")]
            //[AllowAnonymous]
            public IActionResult Validate(Login login)
            {
                try
                {
                    User user = userService.ValidteUser(login.Email, login.Password);
                    AuthReponse authReponse = new AuthReponse();
                    if (user != null)
                    {
                        authReponse.UserId = user.UserId;
                        authReponse.UserName = user.UserName;
                        authReponse.Role = user.role;
                        authReponse.Token = GetToken(user);
                    }
                    return StatusCode(200, authReponse);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, ex.InnerException.Message);
                }
            }
            private string GetToken(User? user)
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                //header part
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                );
                //payload part
                var subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Surname,user.UserName),
                        new Claim(ClaimTypes.Role, user.role),
                        new Claim(ClaimTypes.Email,user.Email),
                    });

                var expires = DateTime.UtcNow.AddMinutes(10);
                //signature part
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = expires,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return jwtToken;
            }
        }

    }

