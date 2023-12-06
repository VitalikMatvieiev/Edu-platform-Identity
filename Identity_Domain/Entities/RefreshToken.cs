using Identity_Domain.Entities.Base;

namespace Identity_Domain.Entities;

public class RefreshToken : Entity
{
    public string Token { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsUsed { get; set; }

    public bool Invalidated { get; set; }

    public Identity Identity { get; set; }
}