namespace Entities.CustomerTaxiService.Requests;

[Serializable]
public class Registration
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}