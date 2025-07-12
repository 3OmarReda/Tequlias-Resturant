using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TequliasResturant.Data;
using TequliasResturant.Models;

namespace TequliasResturant.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;
        public IngredientController(ApplicationDbContext context)
        {
            this.ingredients = new Repository<Ingredient>(context);
        }
        public async Task<IActionResult> Index()
        {
            return View("Index", await ingredients.GetAllAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View("Details", await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient>() {Includes="ProductIngredients.Product" }));// to include all products));
        }
        //Ingrdient/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // For Security
        public async Task<IActionResult> Create([Bind("IngredientId, Name")]Ingredient ingredient)
		//Bind ==> من الفورم (IngredientId, Name)  أنا عايز أستقبل فقط الحقول دي 
		{
			if (ModelState.IsValid)
            {
                await ingredients.AddAsync(ingredient);
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }
        //Ingredient/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient>() { Includes = "ProductIngredients.Product" }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Ingredient ingredient)
        {
            await ingredients.DeleteAsync(ingredient.IngredientId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient>{ Includes = "ProductIngredients.Product" }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await ingredients.UpgateAsync(ingredient);
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }
	}
}
