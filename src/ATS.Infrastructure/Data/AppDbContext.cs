using ATS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace ATS.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Candidate>(cm =>
        {
            cm.ToCollection("candidates");
            cm.HasKey(candidate => candidate.Id);
        });
    }
}
