using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingProject.WebMvc.Models;

/**********************
    -- TRANSACTION TABLE
    CREATE TABLE [dbo].[Products]
    (
        [ProductId] uniqueidentifier
        , [ProductName] nvarchar(100) NOT NULL
        , [Price] float NOT NULL
        , [CId] int NOT NULL                -- CategoryID SHOULD HAVE SAME DATATYPE and NOT NULL

        , CONSTRAINT [PK_Products]
            PRIMARY KEY ( [ProductId] ASC )
        , CONSTRAINT [FK_Products_Categories_CId]
            FORIEGN KEY [CId] REFERENCES [Categories] ( [CategoryId] )
    )
***/

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


    #region Navigation Properties to the Category Model

    [Required]
    public int CId { get; set; }

    [ForeignKey(nameof(Product.CId))]
    public Category? Category { get; set; }

    #endregion
}
