using Demo_Banking.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;


namespace Demo_Banking.Pages;

public class MyRazorPageModel : PageModel
{
    public string CompanyName { get; set; } = "Microsoft";

    public InputModel? Input { get; set; }


    public class InputModel
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
    }


    public void OnGet()
    {
        this.Input = new InputModel()
        {
            ProductName = "Some demo product",
            Price = 50.35M
        };
    }

    public void OnPost()
    {

    }

}
