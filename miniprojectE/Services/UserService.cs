using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using miniprojectE.Data;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.DTO.UserDTOs;
using miniprojectE.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace miniprojectE.Services
{
        public class UserService : IUserService
        {
            private readonly AppDB _context;
            private readonly IConfiguration _configuration;

            public UserService(AppDB context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<UserProfileDTO> RegisterUserAsync(UserRegistrationDTO dto)
            {
                // Check if user already exists
                if (await _context.User.AnyAsync(u => u.UserEmail == dto.UserEmail))
                {
                    throw new ArgumentNullException("User with this email already exists");
                }

                // Hash password
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                var user = new Users
                {
                    UserEmail = dto.UserEmail,
                    Password = passwordHash,
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Phone = dto.Phone,
                    Role = UserType.Customer,
                    IsActive = true,
                    RegistrationDate = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                return new UserProfileDTO
                {
                    UserId = user.UserID,
                    UserEmail = user.UserEmail,
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    UserType = user.Role,
                    CreatedAt = user.RegistrationDate
                };
            }

            public async Task<(string Token, UserProfileDTO user)> LoginAsync(UserLoginDTO dto)
            {
                var user = await _context.User
                    .FirstOrDefaultAsync(u => u.UserEmail == dto.UserEmail && u.IsActive);

                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                {
                    throw new UnauthorizedAccessException("Invalid email or password");
                }

            var userdto = new UserProfileDTO
            {
                UserId = user.UserID,
                UserEmail = user.UserEmail,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                UserType = user.Role,
                CreatedAt = user.RegistrationDate,
                Addresses = user.Addresses.Select(a => new AddressDTO
                {
                    AddressID = a.AddressID,
                    Street = a.Street,
                    City = a.City,
                    Province = a.Province,
                    Code = a.Code,
                    Country = a.Country
                    //ty = a.AddressType,
                    //isDefault = a.IsDefault
                }).ToList(),
            };

                // Generate JWT token
                return (GenerateJwtToken(user), userdto);
            }

            public async Task<UserProfileDTO> GetUserProfileAsync(Guid userId)
            {
                var user = await _context.User
                    .Include(u => u.Addresses)
                    .FirstOrDefaultAsync(u => u.UserID == userId);

                if (user == null)
                {
                    throw new NotFoundException("User not found");
                }

                return new UserProfileDTO
                {
                    UserId = user.UserID,
                    UserEmail = user.UserEmail,
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    UserType = user.Role,
                    CreatedAt = user.RegistrationDate,
                    Addresses = user.Addresses.Select(a => new AddressDTO
                    {
                        AddressID = a.AddressID,
                        Street = a.Street,
                        City = a.City,
                        Province = a.Province,
                        Code = a.Code,
                        Country = a.Country
                        //ty = a.AddressType,
                        //isDefault = a.IsDefault
                    }).ToList()
                };
            }

            public async Task<UserProfileDTO> UpdateUserAsync(Guid userId, UserUpdateDTO dto)
            {
                var user = await _context.User.FindAsync(userId);
                if (user == null)
                {
                    //throw new NotFoundException("User not found");
                }

                user.Name = dto.FirstName;
                user.LastName = dto.LastName;
                user.Phone = dto.Phone;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return await GetUserProfileAsync(userId);
            }

            public async Task<List<UserProfileDTO>> GetUsersByRoleAsync(UserType userType)
            {
                var users = await _context.User
                    .Where(u => u.Role == userType && u.IsActive)
                    .Select(u => new UserProfileDTO
                    {
                        UserId = u.UserID,
                        UserEmail = u.UserEmail,
                        Name = u.Name,
                        LastName = u.LastName,
                        Phone = u.Phone,
                        UserType = u.Role,
                        CreatedAt = u.RegistrationDate
                    })
                    .ToListAsync();

                return users;
            }

            private string GenerateJwtToken(Users user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}")
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
}
