namespace ASPLabb3.Models.DTO;

public class PeopleDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Phone { get; set; }
	public IEnumerable<InterestDto> Interests { get; set; }
}
