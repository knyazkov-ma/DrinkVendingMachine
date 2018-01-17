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
                shoppingСartService.GetShowcaseDrinks(),
                shoppingСartService.GetCoins(),
                shoppingСartService.GetSurrender(),
                shoppingСartService.GetShoppingСartTotalPayment(),
                shoppingСartService.GetShoppingСartTotalCost()
                );
        }

        public ActionResult Index()
        {
            shoppingСartService.Init();
            return View("Index", getShoppingСartModel());
        }

        public ActionResult SelectDrink(long drinkId)
        {
            shoppingСartService.SelectDrink(drinkId);
            return View("Index", getShoppingСartModel());
        }

        public ActionResult Pay(long coinId)
        {
            shoppingСartService.Pay(coinId);
            return View("Index", getShoppingСartModel());
        }

        public ActionResult Purchase()
        {
            shoppingСartService.Purchase();
            shoppingСartService.Init();
            return View("Index", getShoppingСartModel());
        }
        
    }
}