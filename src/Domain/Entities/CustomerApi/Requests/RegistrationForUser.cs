using System.ComponentModel.DataAnnotations;
using Entities.CustomerApi.CustomerData.Interface;

namespace Domain.Entities.CustomerApi.Requests;

[Serializable]
public class RegistrationForUser : ICustomerBase
{
    [Required]
    [StringLength(15)]
    public string Name { get; set; }
    [Required]
    [StringLength(15)]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
    public string Email { get; set; }
}