using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace BankingProject.WebMvc.DtoModels;

public class CategoryDtoModel
{
    [Required]
    public int CategoryId { get; set; }


    [Display(Name = "Category Name")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(50, ErrorMessage = "{0} cannot contain more than {1} characters.")]
    public string CategoryName { get; set; } = string.Empty;


    [Display(Name = "Description")]
    public string? CategoryDescription { get; set; }
}
