using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.Models;
using System.Web.Mvc;

namespace DrinkVendingMachine.Controllers
{
    public class ShoppingСartController : Controller
    {
        private readonly IShoppingСartService shoppingСartService;
        public ShoppingСartController(IShoppingСartService shoppingСartService)
        {
            this.shoppingСartService = shoppingСartService;
        }

        private ShoppingСartModel getShoppingСartModel()
        {
            return new ShoppingСartModel(
                shoppingСartService.GetDrinks(),
                shoppingСartService.GetCoins(),
                shoppingСartService.GetSurrender(),
                shoppingСartService.GetShoppingСartTotalPayment(),
                shoppingСartService.GetShoppingСartTotalCost()
                );
        }

        public ActionResult Index()
        {
            shoppingСartService.Init();
            return View(getShoppingСartModel());
        }
                
        public ActionResult SelectDrink(long id)
        {
            shoppingСartService.SelectDrink(id);
            return View("Index", getShoppingСartModel());
        }
                
        public ActionResult Pay(long id)
        {
            shoppingСartService.Pay(id);
            return View("Index", getShoppingСartModel());
        }

        [HttpPost]
        public ActionResult Purchase()
        {
            shoppingСartService.Purchase();
            shoppingСartService.Init();
            return View("Index", getShoppingСartModel());
        }
        
    }
}