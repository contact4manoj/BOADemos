using BankingProject.WebMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingProject.WebMvc.Controllers;

public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(
        ILogger<CategoriesController> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        // return View(await _context.Categories.ToListAsync());

        var categoriesWithProducts = await _context.Categories
                                                   .Include(categories => categories.Products)
                                                   .ToListAsync();
        return View(categoriesWithProducts);
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(int? id)
    {         
        if (id is null)         // if( id == null )   => if ( id.GetType() == typeof(null) )
        {
            return NotFound();
        }

        // SELECT TOP(1) * FROM [Categories] WHERE [CategoryId] == id
        // var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

        // SELECT TOP(1) * FROM [Categories] WHERE [CategoryId] == id
        // var category = await _context.Categories.FindAsync(id);      // only Primary Key

        // SELECT TOP(2) * FROM [Categories] WHERE [CategoryId] == id => EF throws exception if more than 1 row returned from DB
        // var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);

        var category = await _context.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("CategoryName,CategoryDescription")] Category category)
    {
        // duplicate checking on CategoryName
        bool isDuplicateFound 
            = _context.Categories.Any(c => c.CategoryName !=  category.CategoryName);
        if (isDuplicateFound)
        {
            ModelState.AddModelError(nameof(Category.CategoryName), "Duplicate Category found!");
        }

        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id, 
        [Bind("CategoryId,CategoryName,CategoryDescription")] Category category)
    {
        if (id != category.CategoryId)
        {
            return NotFound();
        }

        //TODO: duplicate check
        //    (a) if CategoryName does not change, then check for duplicate
        //    (b) if CategoryName changes, then check for duplicate


        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var category = await _context.Categories
                                     .FirstOrDefaultAsync(m => m.CategoryId == id);
        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.CategoryId == id);
    }
}
