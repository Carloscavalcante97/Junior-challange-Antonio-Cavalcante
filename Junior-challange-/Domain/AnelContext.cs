using Junior_Challenge.Communication.Request;
using Microsoft.EntityFrameworkCore;

namespace Junior_Challenge.Domain;

public class AnelContext : DbContext
{
    public AnelContext(DbContextOptions<AnelContext> options) : base(options)
    {
    }
    public DbSet<Anel> Aneis { get; set; }

}
