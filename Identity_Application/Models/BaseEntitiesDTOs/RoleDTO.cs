namespace Identity_Application.Models.BaseEntitiesDTOs;

public class RoleDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<RoleClaimDTO> Claims { get; set; }
}

public class RoleClaimDTO
{
    public int Id { get; set; }

    public string Name { get; set; }
}