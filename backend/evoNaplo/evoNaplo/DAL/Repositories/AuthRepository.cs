using evoNaplo.DAL.Interfaces;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;
using evoNaplo.Data;
using evoNaplo.Exceptions;

namespace evoNaplo.DAL.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AnyUsersAsync()
    {
        return await _context.Users.AnyAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<Mentor?> GetMentorByEmailAsync(string email)
    {
        return await _context.Mentors.FirstOrDefaultAsync(m => m.Email == email);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

}
