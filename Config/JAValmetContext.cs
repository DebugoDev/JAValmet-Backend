using Microsoft.EntityFrameworkCore;

namespace JAValmet_Backend.Config;

public partial class JAValmetContext(DbContextOptions<JAValmetContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}