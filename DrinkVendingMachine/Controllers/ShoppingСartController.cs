using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.DTO;
using DrinkVendingMachine.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkVendingMachine.Controllers
{
    public class ShoppingСartController : BaseController
    {
        private readonly IShoppingСartService shoppingСartService;
        public ShoppingСartController(IShoppingСartService shoppingСartService)
        {
            this.shoppingСartService = shoppingСartService;
        }

        private ShoppingСartModel getShoppingСartModel()
        {
            IList<SurrenderCoinDTO> surrender = new List<SurrenderCoinDTO>();
            bool canbeSurrender = shoppingСartService.GetSurrender(ref surrender);

            return new ShoppingСartModel(
                shoppingСartService.GetDrinks(),
                shoppingСartService.GetCoins(),
                surrender,
                canbeSurrender,
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