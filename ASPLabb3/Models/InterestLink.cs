﻿namespace ASPLabb3.Models;

public class InterestLink
{
	public int LinkId { get; set; }
	public Link Link { get; set; }
	public int InterestId { get; set; }
	public Interest Interest { get; set; }
}
