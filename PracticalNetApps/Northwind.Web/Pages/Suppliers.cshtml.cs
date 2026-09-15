using Microsoft.AspNetCore.Mvc.RazorPages;
using EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        private NorthwindContext db;

        public IEnumerable<Supplier>? Suppliers {get;set;}

        public SuppliersModel(NorthwindContext injectedContext)
        {
            db = injectedContext;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind - Suppliers";

            Suppliers = db.Suppliers.OrderBy(s=>s.Country).ThenBy(s=>s.CompanyName);
        }

        [BindProperty]
        public Supplier? Supplier { get; set; }
        
        public IActionResult OnPost()
        {
            if(Supplier is not null && ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();

                return RedirectToPage("/Suppliers");
            }
            else
            {
                return Page();
            }
        }
    }
}
