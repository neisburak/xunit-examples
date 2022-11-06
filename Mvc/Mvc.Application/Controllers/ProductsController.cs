using Microsoft.AspNetCore.Mvc;
using Mvc.Application.Models;
using Mvc.Application.Repository;

namespace Mvc.Application.Controllers;

[Route("[controller]")]
public class ProductsController : Controller
{
    private readonly IGenericRepository<Product> _repository;

    public ProductsController(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
        return View(await _repository.GetAll());
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var product = await _repository.GetById((int)id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,Color")] Product product)
    {
        if (ModelState.IsValid)
        {
            await _repository.Create(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var product = await _repository.GetById((int)id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name,Price,Stock,Color")] Product product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _repository.Update(product);

            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var product = await _repository.GetById((int)id);
        if (product is null) return NotFound();

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _repository.GetById(id);
        if (product is null) return NotFound();

        _repository.Delete(product);

        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        var product = _repository.GetById(id).Result;

        if (product == null) return false;
        else return true;
    }
}
