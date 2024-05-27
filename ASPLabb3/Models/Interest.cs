using System.ComponentModel.DataAnnotations;

namespace ASPLabb3.Models;

public class Interest
{
	public int Id { get; set; }

	[StringLength(30, MinimumLength = 3, ErrorMessage = "Title must be within 3 - 30 in length")]
	public string Title { get; set; }

	[StringLength(50, MinimumLength = 3, ErrorMessage = "Description must be within 3 - 30 in length")]
	public string Description { get; set; }
	public ICollection<PeopleInterest> PeopleInterests { get; set; } = [];
	public ICollection<InterestLink> InterestLinks { get; set; } = [];
}