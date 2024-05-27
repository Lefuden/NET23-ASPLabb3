using ASPLabb3.Models;
using Microsoft.EntityFrameworkCore;
namespace ASPLabb3.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PeopleInterest>().HasKey(pi => new { pi.InterestId, pi.PeopleId });
		modelBuilder.Entity<InterestLink>().HasKey(il => new { il.LinkId, il.InterestId });
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<People> Peoples { get; set; }
	public DbSet<Interest> Interests { get; set; }
	public DbSet<Link> Links { get; set; }
	public DbSet<PeopleInterest> PeopleInterests { get; set; }
	public DbSet<InterestLink> InterestLinks { get; set; }
}
