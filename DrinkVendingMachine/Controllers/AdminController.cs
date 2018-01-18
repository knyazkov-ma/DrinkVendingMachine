using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.DTO;
using DrinkVendingMachine.Models;
using System.Web.Mvc;
using System.Linq;

namespace DrinkVendingMachine.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        private AdminModel getAdminModel()
        {
            return new AdminModel(
                adminService.GetDrinks(),
                adminService.GetCoins()
                );
        }
        public ActionResult Index()
        {
            return View(getAdminModel());
        }
        
        public ActionResult Drink(long id)
        {
            DrinkDTO dto = adminService.GetDrink(id);
            DrinkModel model = new DrinkModel()
            {
                Id = dto.Id,
                Cost = dto.Cost,
                Count = dto.Count,
                Ord = dto.Ord
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Drink(DrinkModel model)
        {
            DrinkDTO dto = new DrinkDTO()
            {
                Id = model.Id,
                Cost = model.Cost,
                Count = model.Count,
                Ord = model.Ord
            };
            adminService.SaveDrink(dto, null);
            return View("Index", getAdminModel());
        }

        [HttpPost]
        public ActionResult DeleteDrink(long id)
        {
            adminService.DeleteDrink(id);
            return View("Index", getAdminModel());
        }
        public ActionResult Coins()
        {
            CoinsModel model = new CoinsModel(adminService.GetCoins());
            return View(model);
        }

        [HttpPost]
        public ActionResult Coins(CoinsModel model)
        {
            CoinDTO[] dtos = model.Coins
                .Select(c => new CoinDTO
                {
                    Id = c.Id,
                    Count = c.Count,
                    Lock = c.Lock,
                    Denomination = c.Denomination
                }).ToArray();
            adminService.UpdateCoins(dtos);
            return View("Index", getAdminModel());
        }
        
    }
}