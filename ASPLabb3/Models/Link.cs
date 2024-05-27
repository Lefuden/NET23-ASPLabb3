using System.ComponentModel.DataAnnotations;

namespace ASPLabb3.Models;

public class Link
{
	public int Id { get; set; }

	[StringLength(50, MinimumLength = 3, ErrorMessage = "Address must be within 3 - 50 in length")]
	public string Address { get; set; }
	public ICollection<InterestLink> InterestLinks { get; set; } = [];
}