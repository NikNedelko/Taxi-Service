using System.ComponentModel.DataAnnotations;

namespace Entities.CustomerTaxiService.Requests;

[Serializable]
public class Registration
{
    [Required]
    [StringLength(15)]
    public string Name { get; set; }
    [Required]
    [StringLength(15)]
    public string LastName { get; set; }
    [Required]
    [Range(100000,999999)]
    public string PhoneNumber { get; set; }
    [Required]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
    public string Email { get; set; }
}