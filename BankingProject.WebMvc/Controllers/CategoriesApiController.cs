using BankingProject.WebMvc.Data;
using BankingProject.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingProject.WebMvc.Controllers;

/// <remarks>
///     In an ASP.NET Core REST API, there is no need to explicitly check if the model state is Valid. 
///     Since the controller class is decorated with the [ApiController] attribute, 
///     it takes care of checking if the model state is valid 
///     and automatically returns 400 response along the validation errors.
///     Example response:
///         {
///             "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
///             "title": "One or more validation errors occurred.",
///             "status": 400,
///             "traceId": "|65b7c07c-4323622998dd3b3a.",
///             "errors": {
///                 "Email": [
///                     "The Email field is required."
///                 ],
///                 "FirstName": [
///                     "The field FirstName must be a string with a minimum length of 2 and a maximum length of 100."
///                 ]
///             }
///         }
/// </remarks>


[Route("api/[controller]")]
[ApiController]
public class CategoriesApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CategoriesApiController> _logger;

    public CategoriesApiController(
        ApplicationDbContext context, 
        ILogger<CategoriesApiController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/CategoriesApi
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            if (_context is null || _context.Categories is null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                                           .ToListAsync();

            // Check if data exists in the Database
            if (categories is null)
            {
                return NotFound();          // RETURN: No data was found            HTTP 404
            }
            return Ok(categories);          // RETURN: OkObjectResult - good result HTTP 200
        }
        catch(Exception exp)
        {
            return BadRequest(exp.Message); // RETURN: BadResult                    HTTP 400
            // return Problem(detail: exp.Message);
        }
    }


    [HttpGet("GetCategoriesWithProducts")]
    public async Task<IActionResult> GetCategoriesWithProducts()
    {
        try
        {
            if (_context is null || _context.Categories is null)
            {
                return NotFound();
            }

            //var categories = await _context.Categories.ToListAsync();

            var categories = await _context.Categories
                                           .Include(c => c.Products)
                                           .ToListAsync();

            // Check if data exists in the Database
            if (categories is null)
            {
                return NotFound();          // RETURN: No data was found            HTTP 404
            }
            return Ok(categories);          // RETURN: OkObjectResult - good result HTTP 200
        }
        catch (Exception exp)
        {
            return BadRequest(exp.Message); // RETURN: BadResult                    HTTP 400
        }
    }


    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        try
        {
            if (_context is null || _context.Categories is null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        catch(Exception exp)
        {
            // non-structured logging entry
            // _logger.LogWarning($"Something went wrong! : {exp.Message}");
            // _logger.LogWarning($"Something went wrong! : " + exp.Message);

            // Structed logging entry
            _logger.LogWarning("Something went wrong! : {message}", exp.Message);

            return BadRequest(exp.Message);
        }
    }

    // PUT: api/Categories/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        // Sanitize the Data
        category.CategoryName = category.CategoryName.Trim();

        if (_context is null || _context.Categories is null)
        {
            return NotFound();
        }

        // Server Side Validation
        bool isDuplicateFound 
            = _context.Categories.Any(c => c.CategoryId != category.CategoryId 
                                           && c.CategoryName == category.CategoryName);
        if (isDuplicateFound)
        {
            ModelState.AddModelError("CategoryName", "Duplicate Category Found!");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Entry(category).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException exp)
            {
                bool checkIfCategoryExists 
                    = _context.Categories?.Any(e => e.CategoryId == id) ?? false;
                if (!checkIfCategoryExists)
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError("PUT", exp.Message);
                }
            }
        }
        
        return BadRequest(ModelState);
    }


    // POST: api/Categories
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<IActionResult> PostCategory(Category category)
    {
        // Sanitize the Data
        category.CategoryName = category.CategoryName.Trim();

        if (_context is null || _context.Categories is null)
        {
            return NotFound();
        }

        // Server Side Validation
        bool isDuplicateFound = _context.Categories.Any(c => c.CategoryName == category.CategoryName);
        if (isDuplicateFound)
        {
            ModelState.AddModelError("CategoryName", "Duplicate Category Found!");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                // return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);

                // To enforce that the HTTP RESPONSE CODE 201 "CREATED", package the response.
                var result = CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
                return Ok(result);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("POST", exp.Message);
            }
        }

        return BadRequest(ModelState);
    }

    // DELETE: api/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        try
        {
            if (_context is null || _context.Categories is null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            // return NoContent();
            // return Accepted(category);
            return Ok(category);
        }
        catch (System.Exception exp)
        {
            ModelState.AddModelError("DELETE", exp.Message);
            return BadRequest(ModelState);
        }
    }

}
