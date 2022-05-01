using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using Application.Dto.User;
using Domain.Aggregates.Product;
using Domain.Aggregates.User;
using System.IdentityModel.Tokens.Jwt;
using Application.Services.Tenant;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Account
{
    public class AccountService : ServiceBase<IUserRepository, User, UserDto>, IAccountService
    {
        private readonly ITenantContextService _tenantContextService;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork uow, ITenantContextService tenantContextService, ILogger<AccountService> logger, IMapper mapper /*IEmailService emailService*/ )
          : base(uow, logger, mapper)
        {
            _tenantContextService = tenantContextService;
            _logger = logger;
            _mapper = mapper;
        }

        public Task ChangePasswordAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> ConfirmPasswordResetAsync(string password, string securityCode)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetTokenAsync(UserDto userDto, string password)
        {
            User? user;

            if (userDto.Username.Contains("@")) // login via email
            {
                user = await Repository.GetByEmailAsync(userDto.Email!);
            }
            else // login via username
            {
                user = await Repository.GetByUsernameAsync(userDto.Username!);
            }

            if (user == null)
            {
                throw new ValidationException(ErrorCode.UserNotFound);
            }

            var isPasswordValid = VerifyHashedPassword(user.PasswordHash, password);

            if (!isPasswordValid)
            {
                throw new ValidationException(ErrorCode.IncorrectUsernameOrPassword);
            }

            userDto.Id = user.Id;
            userDto.Username = user.Username;
            userDto.Email = user.Email;
            userDto.CreatedAt = user.CreatedAt;

            return GenerateToken(userDto);
        }

        public async Task<UserDto> GetUserAsync()
        {
            var user = await Repository.GetByIdAsync(_tenantContextService.TenantContext.UserId!);

            if (user == null)
            {
                throw new ValidationException(ErrorCode.UserNotFound);
            }

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public Task<Guid?> GetUserIdAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(UserDto userDto, string password)
        {
            var userByName = await Repository.GetByUsernameAsync(userDto.Username.Normalize());

            if (userByName != null)
            {
                throw new ValidationException(ErrorCode.UserAlreadyExists);
            }

            var userByEmail = await Repository.GetByEmailAsync(userDto.Email.Normalize());

            if (userByEmail != null)
            {
                throw new ValidationException(ErrorCode.UserAlreadyExists);
            }

            userDto.PasswordHash = HashPassword(password);

            var user = _mapper.Map<User>(userDto);

            await Repository.InsertOneAsync(user);

            userDto.Id = user.Id;
        }

        public Task ResetAccountAsync(string emailOrUsername)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;

            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return AreHashesEqual(buffer3, buffer4);
        }

        private string GenerateToken(UserDto user)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();

            var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.String);
            var emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String);

            claims.Add(new Claim("id", user.Id));
            claims.Add(new Claim("username", user.Username));
            claims.Add(new Claim("subscription", user.Subscription.ToString()));
            claims.Add(new Claim("subscription_exp", user.SubscriptionExpiresAt.ToString("MMddyyy")));

            claims.Add(nameIdentifierClaim);
            claims.Add(emailClaim);

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenConstants.Issuer,
                Audience = JwtTokenConstants.Audience,
                SigningCredentials = JwtTokenConstants.SigningCredentials,
                Subject = identity,
                Expires = DateTime.UtcNow.Add(JwtTokenConstants.TokenExpirationTime),
                NotBefore = DateTime.UtcNow
            });

            return handler.WriteToken(securityToken);
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
