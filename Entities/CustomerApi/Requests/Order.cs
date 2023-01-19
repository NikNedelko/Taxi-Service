namespace Entities.CustomerApi.Requests;

[Serializable]
public class Order
{
    public string PhoneNumber { get; set; }
    public string RideEndPoint { get; set; }
}