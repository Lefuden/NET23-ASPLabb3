using ASPLabb3.Models;

namespace ASPLabb3.Data;

public class DbInitializer(ApplicationDbContext _context)
{
	private readonly ApplicationDbContext context = _context;
	public async Task InitDb()
	{
		await context.Database.EnsureCreatedAsync();
		if (context.Peoples.Any())
		{
			return;
		}

		await InitPeoples();
		await InitInterest();
		await InitLinks();
		await InitPeopleInterests();
		await InitInterestsLinks();
	}

	private async Task InitPeoples()
	{
		var people = new List<People>
		{
			new() { Name = "Karl", Phone = "070123456" },
			new() { Name = "Olle", Phone = "070654321" }
		};

		foreach (var p in people)
		{
			await context.Peoples.AddAsync(p);
		}
		await context.SaveChangesAsync();
	}

	private async Task InitInterest()
	{
		var interest = new List<Interest>
		{
			new() { Title = "Football", Description = "Kick balls" },
			new() { Title = "Handball", Description = "Throw balls" }
		};

		foreach (var i in interest)
		{
			await context.Interests.AddAsync(i);
		}
		await context.SaveChangesAsync();
	}

	private async Task InitLinks()
	{
		var links = new List<Link>
		{
			new() { Address = "www.siteaboutfootball.com" },
			new() { Address = "www.siteabouthandball.com" },
			new() { Address = "www.siteaboutballsofallkinds.com" }
		};

		foreach (var l in links)
		{
			await context.Links.AddAsync(l);
		}
		await context.SaveChangesAsync();
	}

	private async Task InitPeopleInterests()
	{
		var peopleInterests = new List<PeopleInterest>
		{
			new() { PeopleId = 1, InterestId = 1, },
			new() { PeopleId = 2, InterestId = 1, },
			new() { PeopleId = 2, InterestId = 2, }
		};

		foreach (var pI in peopleInterests)
		{
			await context.PeopleInterests.AddAsync(pI);
		}
		await context.SaveChangesAsync();
	}

	private async Task InitInterestsLinks()
	{
		var interestLinks = new List<InterestLink>
		{
			new() { InterestId = 1, LinkId = 1},
			new() { InterestId = 1, LinkId = 3},
			new() { InterestId = 2, LinkId = 2}
		};

		foreach (var iL in interestLinks)
		{
			await context.InterestLinks.AddAsync(iL);
		}
		await context.SaveChangesAsync();
	}
}
