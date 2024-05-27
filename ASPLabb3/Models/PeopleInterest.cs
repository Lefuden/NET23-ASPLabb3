namespace ASPLabb3.Models;

public class PeopleInterest
{
	public int PeopleId { get; set; }
	public People People { get; set; }
	public int InterestId { get; set; }
	public Interest Interest { get; set; }

}
