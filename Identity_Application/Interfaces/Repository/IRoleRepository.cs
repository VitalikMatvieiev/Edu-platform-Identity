using Identity_Domain.Entities.Base;

namespace Identity_Application.Interfaces.Repository;

public interface IRoleRepository
{
    Task<ICollection<Role>> GetAllRoles();

    Task<Role> CreateRole(Role toCreate);

    Task<Role> GetRoleById(int roleId);

    Task<Role> UpdateRole(int roleId, string name);

    Task DeleteRole(int roleId);
}