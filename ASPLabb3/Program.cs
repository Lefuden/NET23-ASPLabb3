using ASPLabb3.Data;
using ASPLabb3.Models;
using ASPLabb3.Models.DTO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup DB connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));


builder.Services.AddScoped<DbInitializer>();

var app = builder.Build();

// Initialize DB
using (var scope = app.Services.CreateScope())
{
	DbInitializer dbInit = scope.ServiceProvider.GetRequiredService<DbInitializer>();
	await dbInit.InitDb();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Hämta alla personer i systemet
app.MapGet("/people", async (ApplicationDbContext context) =>
{
	var people = await context.Peoples
		.Include(p => p.PeopleInterests)
			.ThenInclude(pi => pi.Interest)
				.ThenInclude(i => i.InterestLinks)
					.ThenInclude(il => il.Link)
		.ToListAsync();

	var peopleDtoList = people.Select(p => new PeopleDto
	{
		Id = p.Id,
		Name = p.Name,
		Phone = p.Phone,
		Interests = p.PeopleInterests.Select(pi => new InterestDto
		{
			Id = pi.Interest.Id,
			Title = pi.Interest.Title,
			Description = pi.Interest.Description,
			Links = pi.Interest.InterestLinks.Select(il => new LinkDto
			{
				Id = il.Link.Id,
				Address = il.Link.Address
			}).ToList(),
		}).ToList(),
	}).ToList();

	return Results.Ok(peopleDtoList);
});

// Hämta alla intressen som är kopplade till en specifik person
app.MapGet("/people/{id:int}/interests", async (ApplicationDbContext context, int id) =>
{
	var interest = await context.PeopleInterests
		.Include(pi => pi.Interest)
		.Where(pi => pi.PeopleId == id)
		.ToListAsync();

	var interestDtoList = interest.Select(i => new InterestDto
	{
		Id = i.Interest.Id,
		Title = i.Interest.Title,
		Description = i.Interest.Description
	}).ToList();

	return Results.Ok(interestDtoList);
});

// Hämta alla länkar som är kopplade till en specifik person
app.MapGet("people/{id:int}/links", async (ApplicationDbContext context, int id) =>
{
	var links = await context.PeopleInterests
		.Include(pi => pi.Interest)
			.ThenInclude(i => i.InterestLinks)
				.ThenInclude(il => il.Link)
		.Where(pi => pi.PeopleId == id)
		.ToListAsync();

	var linkDtoList = links.Select(il => il.Interest.InterestLinks.Select(l => new LinkDto
	{
		Id = l.Link.Id,
		Address = l.Link.Address
	}).ToList()
	).ToList();

	return Results.Ok(linkDtoList);
});

// Koppla en person till ett nytt intresse
app.MapPost("/people/{id:int}/interest", async (ApplicationDbContext context, int id, AddInterestDto addInterestDto) =>
{
	Interest newInterest = new() { Title = addInterestDto.Title, Description = addInterestDto.Description };
	await context.AddAsync(newInterest);
	await context.SaveChangesAsync();

	var newPeopleInterest = new PeopleInterest
	{
		PeopleId = id,
		InterestId = newInterest.Id
	};

	await context.AddAsync(newPeopleInterest);
	await context.SaveChangesAsync();

	return Results.Created();
});

// Lägga in nya länkar för en specifik person och ett specifikt intresse
app.MapPost("/people/{pId:int}/interest/{iId:int}/links",
	async (ApplicationDbContext context, int pId, int iId, string address) =>
	{
		var interest = await context.PeopleInterests
			.Include(pi => pi.Interest)
			.Where(pi => pi.PeopleId == pId && pi.InterestId == iId)
			.Select(pi => pi.Interest)
			.FirstOrDefaultAsync();

		if (interest == null) return Results.NotFound();

		var interestLinkExists = await context.InterestLinks
			.Include(il => il.Link)
			.Where(il => il.Link.Address == address && il.InterestId == iId)
			.ToListAsync();

		if (interestLinkExists.Count > 0) return Results.BadRequest();

		Link newLink = new() { Address = address };
		await context.AddAsync(newLink);
		await context.SaveChangesAsync();

		InterestLink newInterestLink = new()
		{
			LinkId = newLink.Id,
			InterestId = interest.Id,
		};

		await context.AddAsync(newInterestLink);
		await context.SaveChangesAsync();

		return Results.Created();
	});

app.Run();
