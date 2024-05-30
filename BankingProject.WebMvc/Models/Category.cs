using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace BankingProject.WebMvc.Models;

/**
    -- MASTER TABLE
    CREATE TABLE [dbo].[Categories]
    (
        [CategoryId] int NOT NULL IDENTITY (1, 1)
        , [CategoryName] varchar(50) NOT NULL
        , [CategoryDescription] nvarchar(MAX) NULL

        , CONSTRAINT [PK_Categories] 
            PRIMARY KEY ( [CategoryId] ASC )
    )
 **/

[Table(name: "Categories", Schema = "dbo")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }


    [Display(Name = "Category Name")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [StringLength(50, ErrorMessage = "{0} cannot contain more than {1} characters.")]
    [Column(TypeName = "varchar(50)")]
    public string CategoryName { get; set; } = string.Empty;


    [Display(Name = "Description")]
    public string? CategoryDescription { get; set; }


    #region Navigation Properties to the Product Model

    public ICollection<Product>? Products { get; set; }

    #endregion
}
