using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Identity_Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IdentityDbContext _context;

    public RoleRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Role> CreateRole(Role toCreate)
    {
        _context.Role.Add(toCreate);
        await _context.SaveChangesAsync();

        return toCreate;
    }

    public async Task DeleteRole(int roleId)
    {
        var role = _context.Role
            .FirstOrDefault(i => i.Id == roleId);

        if (role is null) return;

        _context.Role.Remove(role);

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Role>> GetAllRoles()
    {
        return await _context.Role.ToListAsync();
    }

    public async Task<Role> GetRoleById(int roleId)
    {
        return await _context.Role.FirstOrDefaultAsync(i => i.Id == roleId);
    }

    public async Task<Role> UpdateRole(int roleId, string name)
    {
        var role = await _context.Role
            .FirstOrDefaultAsync(i => i.Id == roleId);

        role.Name = name;

        await _context.SaveChangesAsync();

        return role;
    }
}