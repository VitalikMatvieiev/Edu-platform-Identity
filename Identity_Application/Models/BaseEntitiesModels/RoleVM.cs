namespace Identity_Application.Models.BaseEntitiesModels;

public class RoleVM
{
    public string Name { get; set; } = string.Empty;

    public int?[] ClaimsIds { get; set; }
}