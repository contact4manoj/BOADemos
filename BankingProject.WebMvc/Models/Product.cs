using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingProject.WebMvc.Models;

[Table(name: "Products", Schema = "dbo")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ProductId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} cannot have more than {1} characters")]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [DefaultValue(0.0)]
    public double Price { get; set; }
}
