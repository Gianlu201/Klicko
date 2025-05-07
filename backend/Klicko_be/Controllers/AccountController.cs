using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.ApplicationRole;
using Klicko_be.DTOs.ApplicationUserRole;
using Klicko_be.Models;
using Klicko_be.Models.Auth;
using Klicko_be.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Jwt _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(
            IOptions<Jwt> jwtOptions,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager
        )
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountByAdminDash()
        {
            try
            {
                var accountsList = await _userManager
                    .Users.Include(a => a.UserRoles)
                    .ThenInclude(ur => ur.ApplicationRole)
                    .ToListAsync();

                if (accountsList == null)
                {
                    return BadRequest(new { Message = "Something went wrong!" });
                }

                var accountsListDto = accountsList
                    .Select(account => new AccountDto()
                    {
                        UserId = account.Id,
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        Email = account.Email,
                        RegistrationDate = account.RegistrationDate,
                        UserRole =
                            account.UserRoles != null && account.UserRoles.Count > 0
                                ? account
                                    .UserRoles.Select(ur => new UserRoleForUserDto()
                                    {
                                        UserRoleId = ur.UserRoleId,
                                        RoleId = ur.ApplicationRole.Id,
                                        RoleName =
                                            ur.ApplicationRole != null
                                                ? ur.ApplicationRole.Name
                                                : null,
                                    })
                                    .FirstOrDefault()
                                : null,
                    })
                    .ToList();

                return accountsList.Count == 0
                    ? BadRequest(
                        new GetAccountsListResponseDto()
                        {
                            Message = "No accounts found!",
                            Accounts = null,
                        }
                    )
                    : Ok(
                        new GetAccountsListResponseDto()
                        {
                            Message = $"{accountsList.Count} accounts found!",
                            Accounts = accountsListDto,
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var rolesList = await _roleManager.Roles.ToListAsync();

                if (rolesList == null)
                {
                    return BadRequest(
                        new GetRolesListResponseDto()
                        {
                            Message = "Something went wrong!",
                            Roles = null,
                        }
                    );
                }

                var rolesListDto = rolesList
                    .Select(role => new RoleDto() { RoleId = role.Id, RoleName = role.Name })
                    .ToList();

                return rolesList.Count == 0
                    ? BadRequest(
                        new GetRolesListResponseDto() { Message = "No roles found!", Roles = null }
                    )
                    : Ok(
                        new GetRolesListResponseDto()
                        {
                            Message = $"{rolesListDto.Count} roles found!",
                            Roles = rolesListDto,
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var newUserId = Guid.NewGuid().ToString();
            var newCartId = Guid.NewGuid();
            var newFidelityId = Guid.NewGuid();

            var newUser = new ApplicationUser()
            {
                Id = newUserId,
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Email,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                RegistrationDate = DateTime.Now,
                CartId = newCartId,
                Cart = new Cart()
                {
                    CartId = newCartId,
                    UserId = newUserId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                FidelityCardId = newFidelityId,
                FidelityCard = new FidelityCard()
                {
                    FidelityCardId = newFidelityId,
                    Points = 0,
                    AvailablePoints = 0,
                    UserId = newUserId,
                },
                Coupons = new List<Coupon>()
                {
                    new Coupon()
                    {
                        CouponId = Guid.NewGuid(),
                        UserId = newUserId,
                        PercentualSaleAmount = 5,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = null,
                        Code = "WELCOME5",
                        MinimumAmount = 100,
                    },
                },
            };

            // se la data attuale di registrazione è precedente al 31/05/2025 aggiunge alla
            // lista coupon associata al nuovo utente un altro coupon speciale in occasione del DEMODAY
            if (newUser.RegistrationDate < new DateTime(2025, 5, 31))
            {
                newUser.Coupons.Add(
                    new Coupon()
                    {
                        CouponId = Guid.NewGuid(),
                        UserId = newUserId,
                        PercentualSaleAmount = 15,
                        FixedSaleAmount = 0,
                        IsActive = true,
                        IsUniversal = false,
                        ExpireDate = DateTime.Parse("31/05/2025 23:59:59"),
                        Code = "DEMODAY15",
                        MinimumAmount = 250,
                    }
                );
            }

            var result = await _userManager.CreateAsync(newUser, registerRequestDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(newUser.Email);

            //await _userManager.AddToRoleAsync(newUser, "Admin");
            await _userManager.AddToRoleAsync(newUser, "User");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginRequestDto.Password,
                false,
                false
            );

            if (result.Succeeded == false)
            {
                return BadRequest(new { Message = "Invalid credentials!" });
            }

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("name", user.FirstName));
            claims.Add(new Claim("surname", user.LastName));
            claims.Add(new Claim("cartId", user.CartId.ToString()));
            claims.Add(new Claim("fidelityCardId", user.FidelityCardId.ToString()));
            claims.Add(new Claim("nameIdentifier", user.Id));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim("role", role));
            //}

            claims.Add(new Claim("role", roles[0]));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponseDto() { Token = tokenString, Expires = expiry });
        }

        [HttpPut("EditRole/{userId:guid}")]
        public async Task<IActionResult> EditUserRole([FromBody] Guid newRoleId, Guid userId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(newRoleId.ToString());

                if (role == null)
                {
                    return BadRequest(new { Message = "Role not found!" });
                }

                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    return BadRequest(new { Message = "User not found!" });
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                }
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    return BadRequest(new { Message = "Something went wrong!" });
                }

                return Ok(
                    new EditUserRoleResponseDto() { Message = "User role updated successfully!" }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
