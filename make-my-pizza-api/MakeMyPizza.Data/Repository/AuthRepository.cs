using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.DataExceptions;
using Microsoft.Extensions.Logging;

namespace MakeMyPizza.Data.Repository;

public class AuthRepository : IAuthRepository
{
    internal PizzaOrderManagementDbContext _context;
    private readonly ILogger<AuthRepository> _logger;
    public AuthRepository(PizzaOrderManagementDbContext context,
                          ILogger<AuthRepository> logger)
    {
        _context = context;
        _logger = logger;

    }

    public async Task<User> Login(string username, string password)
    {
        _logger.LogInformation($"Login started for username {username}");
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
        if (user == null)
        {
            _logger.LogInformation($"user not found for username {username}");
            throw new RecordNotFoundException("Invalid credentials.");
        }
        else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            _logger.LogInformation($"user password did not match for username {username}");
            throw new AuthenticationException("Invalid credentials.");
        }
        else
        {
            _logger.LogInformation($"user login success for username {username}");
            return user;
        }
    }

    public async Task<User> Register(User user, string password)
    {
        _logger.LogInformation($"Register user started for username {user.Username}");
        if (await UserExists(user.Username))
        {
            _logger.LogInformation($"User already exists for username {user.Username}");
            throw new RecordAlreadyExistsException("That username is taken. Try another.");
        }

        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"User registered successfully for username {user.Username}");
        return user;
    }

    public async Task<bool> UserExists(string username)
    {
        if (await _context.Users.AnyAsync(x => x.Username.ToLower().Equals(username.ToLower())))
        {
            return true;
        }
        return false;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}