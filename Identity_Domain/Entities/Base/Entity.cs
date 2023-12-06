namespace Identity_Domain.Entities.Base;

/// <summary>
/// Base class for all entities, that should have Id
/// https://enterprisecraftsmanship.com/posts/entity-base-class/
/// </summary>
public abstract class Entity
{
    public virtual int Id { get; protected set; }

    protected Entity()
    {
    }

    protected Entity(int id)
    {
        Id = id;
    }

    public override bool Equals(object obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetUnproxiedType(this) != GetUnproxiedType(other))
            return false;

        if (Id.Equals(default) || other.Id.Equals(default))
            return false;

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetUnproxiedType(this).ToString() + Id).GetHashCode();
    }

    /// <summary>
    /// It’s here to address the issue of ORMs returning the type of a runtime proxy when you call GetType() on an object. 
    /// This method returns the underlying, real type of the entity. It handles both NHibernate and EF Core.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    internal static Type GetUnproxiedType(object obj)
    {
        const string EFCoreProxyPrefix = "Castle.Proxies.";
        const string NHibernateProxyPostfix = "Proxy";

        Type type = obj.GetType();
        string typeString = type.ToString();

        if (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
            return type.BaseType;

        return type;
    }
}