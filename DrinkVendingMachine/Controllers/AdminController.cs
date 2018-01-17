using DrinkVendingMachine.DataService.Interface;
using System.Web.Mvc;

namespace DrinkVendingMachine.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public ActionResult Index()
        {
            return View("Index");
        }
    }
}