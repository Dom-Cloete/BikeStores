using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using domBikeStores.Models;
using domBikeStores.ViewModels;

namespace domBikeStores.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: Home
        public async Task<ActionResult> Index(string brandFilter, string categoryFilter, int staffPage = 1, int custPage = 1, int prodPage = 1)
        {
            const int pageSize = 3;

            var staffQuery = db.staffs.Include(s => s.stores)
                                      .Include(s => s.orders.Select(o => o.order_items.Select(oi => oi.products)))
                                      .OrderBy(s => s.staff_id);
            var customerQuery = db.customers
                                  .Include(c => c.orders.Select(o => o.order_items.Select(oi => oi.products)))
                                  .OrderBy(c => c.customer_id);
            var productQuery = db.products.Include(p => p.brands)
                                          .Include(p => p.categories)
                                          .AsQueryable();

            if (!string.IsNullOrEmpty(brandFilter))
                productQuery = productQuery.Where(p => p.brands.brand_name == brandFilter);
            if (!string.IsNullOrEmpty(categoryFilter))
                productQuery = productQuery.Where(p => p.categories.category_name == categoryFilter);

            productQuery = productQuery.OrderBy(p => p.product_id);

            var viewModel = new HomeViewModel
            {
                StaffList = await staffQuery.Skip((staffPage - 1) * pageSize).Take(pageSize).ToListAsync(),
                CustomerList = await customerQuery.Skip((custPage - 1) * pageSize).Take(pageSize).ToListAsync(),
                ProductList = await productQuery.Skip((prodPage - 1) * pageSize).Take(pageSize).ToListAsync(),
                BrandList = await db.brands.ToListAsync(),
                CategoryList = await db.categories.ToListAsync(),
                SelectedBrand = brandFilter,
                SelectedCategory = categoryFilter,
                StaffPage = staffPage,
                CustPage = custPage,
                ProdPage = prodPage
            };

            return View(viewModel);
        }

        // POST: Create Staff (called from modal)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStaff(staffs staff)
        {
            if (ModelState.IsValid)
            {
                db.staffs.Add(staff);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // POST: Create Customer (called from modal)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomer(customers customer)
        {
            if (ModelState.IsValid)
            {
                db.customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}