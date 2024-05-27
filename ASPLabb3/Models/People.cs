using System.ComponentModel.DataAnnotations;

namespace ASPLabb3.Models;

public class People
{
	public int Id { get; set; }

	[StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be within 3 - 30 in length")]
	public string Name { get; set; }
	public string Phone { get; set; }
	public ICollection<PeopleInterest> PeopleInterests { get; set; } = [];
}
