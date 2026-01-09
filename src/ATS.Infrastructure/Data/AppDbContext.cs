using ATS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace ATS.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Candidate>(cm =>
        {
            cm.ToCollection("candidates");
            cm.HasKey(candidate => candidate.Id);
        });

        modelBuilder.Entity<Job>(jm =>
        {
            jm.ToCollection("jobs");
            jm.HasKey(job => job.Id);
            jm.Property(job => job.Status).HasConversion<string>();
        });

        modelBuilder.Entity<JobApplication>(jam =>
        {
            jam.ToCollection("job_applications");
            jam.HasKey(ja => ja.Id);

            jam.HasIndex(ja => ja.CandidateId);
            jam.HasIndex(ja => ja.JobId);

            jam.Property(ja => ja.Status).HasConversion<string>();
        });
    }
}
