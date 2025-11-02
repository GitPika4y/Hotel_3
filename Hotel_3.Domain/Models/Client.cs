namespace Hotel_3.Domain.Models;

public class Client : EntityObject
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string MiddleName { get; set; } = null!;
	public DateTime CreatedAt { get; set; } =  DateTime.Now;
}