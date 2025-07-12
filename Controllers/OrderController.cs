using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TequliasResturant.Data;
using TequliasResturant.Models;

namespace TequliasResturant.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _context;
		private Repository<Product> _product;
		private Repository<Order> _order;
		private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
			_context = context;
			_userManager = userManager;
			_product = new Repository<Product>(context);
			_order = new Repository<Order>(context);
        }
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.Products = await _product.GetAllAsync();

			//Retrieve or create an OrderViewModel from session or other state managment
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
			{
				OrderItems = new List<OrderItemViewModel>(),
				Products =await _product.GetAllAsync()
			};
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddItem(int prodId,int prodQty)
		{
            var product = await _context.Products.FindAsync(prodId);
            if (product == null)
            {
                return NotFound();
            }

            // Retrieve or create an OrderViewModel from session or other state management
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _product.GetAllAsync()
            };

            // Check if the product is already in the order
            var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

            // If the product is already in the order, update the quantity
            if (existingItem != null)
            {
                existingItem.Quantity += prodQty;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = prodQty,
                    ProductName = product.Name
                });
            }

            // Update the total amount
            model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            // Save updated OrderViewModel to session
            HttpContext.Session.Set("OrderViewModel", model);

            // Redirect back to Create to show updated order items
            return RedirectToAction("Create", model);

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            //Retrieve the OrderViewModel from session or other state managment
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if(model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
            if(model == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }
            //Create a new Order entity
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                UserId = _userManager.GetUserId(User)
            };

            //Add OrderItems to the Order entity
            foreach (var item in model.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
            //Save the order entity to the database
            await _order.AddAsync(order);

            //clear the OrderViewModel from session or other state managment
            HttpContext.Session.Remove("OrderViewModel");

            return RedirectToAction("ViewOrders");
        }
        [HttpGet]
        [Authorize]
       public async Task<IActionResult> ViewOrders()
        {
            var userId = _userManager.GetUserId(User);

            var userOrders = await _order.GetAllByIdAsync(userId, "UserId", new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });
            return View(userOrders);
        }
	}
}
