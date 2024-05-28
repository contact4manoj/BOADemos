using System.ComponentModel.DataAnnotations;

namespace Demo_Banking.ViewModels;

public class Employee
{

    public int Id { get; set; }

    [Display(Name = "Employee Name:")]
    [Required(ErrorMessage="{0} cannot be empty.")]
    [MinLength(2, ErrorMessage = "{0} should have at least {1} characters.")]
    [MaxLength(50, ErrorMessage = "{0} cannot have more than {1} characters.")]
    public string Name { get; set; } = string.Empty;
}
