using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingProject.WebMvc.ViewModels;

public class CategoriesListViewModel
{
    public int CategoryId { get; set; }


    [Display(Name = "Category Name")]
    public string CategoryName { get; set; } = string.Empty;


    [Display(Name = "Description")]
    public string? CategoryDescription { get; set; }


    [Display(Name = "Number of Products")]
    public int NumberOfProducts { get; set; }
}
