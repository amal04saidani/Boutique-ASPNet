using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Boutique.Data;
using Boutique.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Boutique.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSizeDefault = 10; // 10 produits par page

        public ProduitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index(string search, string sortOrder, int? page, int? pageSize)
        {
            int pageNumber = page ?? 1;
            int pageSizeUsed = pageSize ?? PageSizeDefault;

            // tri : default = name asc
            ViewData["CurrentFilter"] = search ?? "";
            ViewData["CurrentSort"] = sortOrder ?? "";
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var query = _context.Produits
                                .Include(p => p.Categorie)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Nom.Contains(search));
            }

            // apply sort
            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(p => p.Nom),
                "price_asc" => query.OrderBy(p => p.Prix),
                "price_desc" => query.OrderByDescending(p => p.Prix),
                _ => query.OrderBy(p => p.Nom),
            };

            // total count for pagination
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSizeUsed);

            var items = await query
                .Skip((pageNumber - 1) * pageSizeUsed)
                .Take(pageSizeUsed)
                .ToListAsync();

            // pass pagination info to view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSizeUsed;
            ViewBag.TotalCount = totalCount;

            return View(items);
        }

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var produit = await _context.Produits
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null) return NotFound();

            return View(produit);
        }

        // GET: Produits/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom");
            return View();
        }

        // POST: Produits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prix,EnStock,CategorieId")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategoriesDropDownList(produit.CategorieId);
            return View(produit);
        }

        // GET: Produits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();
            PopulateCategoriesDropDownList(produit.CategorieId);
            return View(produit);
        }

        // POST: Produits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prix,EnStock,CategorieId")] Produit produit)
        {
            if (id != produit.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateCategoriesDropDownList(produit.CategorieId);
            return View(produit);
        }

        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var produit = await _context.Produits
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null) return NotFound();

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.Id == id);
        }

        private void PopulateCategoriesDropDownList(object? selectedCategory = null)
        {
            var categoriesQuery = from c in _context.Categories
                                  orderby c.Nom
                                  select c;
            ViewBag.Categories = new SelectList(categoriesQuery.AsNoTracking(), "Id", "Nom", selectedCategory);
        }
    }
}
