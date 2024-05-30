using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankingProject.WebMvc.Models;

public class BankingUser : IdentityUser
{

    [Required]
    [StringLength(40, ErrorMessage = "{0} cannot have more than {1} characters.")]
    [MinLength(2, ErrorMessage = "{0} should have at least {1} characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "{0} cannot have more than {1} characters.")]
    public string LastName { get; set; } = string.Empty;

}
