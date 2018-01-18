using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.DTO;
using DrinkVendingMachine.Models;
using System.Web.Mvc;
using System.Web;
using System.IO;

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
        public ActionResult Drink(DrinkModel model, HttpPostedFileBase fileUpload)
        {
            DrinkDTO dto = new DrinkDTO()
            {
                Id = model.Id,
                Cost = model.Cost,
                Count = model.Count,
                Ord = model.Ord,
            };
            byte[] image = null;
            if (fileUpload != null)
            {
                using (var binaryReader = new BinaryReader(fileUpload.InputStream))
                {
                    image = binaryReader.ReadBytes(fileUpload.ContentLength);
                }
            }
            
            adminService.SaveDrink(dto, image);
            return View("Index", getAdminModel());
        }

        public ActionResult DeleteDrink(long id)
        {
            adminService.DeleteDrink(id);
            return View("Index", getAdminModel());
        }
        public ActionResult Coin(long id)
        {
            CoinDTO dto = adminService.GetCoin(id);
            CoinModel model = new CoinModel
            {
                 Id = dto.Id,
                 Count = dto.Count,
                 Denomination = dto.Denomination,
                 Lock = dto.Lock
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Coin(CoinModel model)
        {
            CoinDTO dto = new CoinDTO
            {
                Id = model.Id,
                Count = model.Count,
                Lock = model.Lock,
                Denomination = model.Denomination
            };

            adminService.UpdateCoin(dto);
            return View("Index", getAdminModel());
        }
        
    }
}