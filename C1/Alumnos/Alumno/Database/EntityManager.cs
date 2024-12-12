using Microsoft.EntityFrameworkCore;

namespace CSharpLocalAndRemote.Database;


public class EntityManager<T> where T : class
{
    public EntityManager(DbContext context)
    {
        Context = context;
    }

    public DbSet<T> DbSet =>
        Context.Set<T>();

    public DbContext Context { get; }
}