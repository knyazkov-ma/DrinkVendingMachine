using DrinkVendingMachine.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;

namespace DrinkVendingMachine.Controllers
{
    
    public class AccountController : Controller
    {
        
                
        private ApplicationSignInManager signInManager;
        
        
        public ApplicationSignInManager SignInManager
        {
            get
            {
                ApplicationSignInManager m = signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                return m;
            }
            private set 
            { 
                signInManager = value; 
            }
        }
        
        
                
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            string password = Request.QueryString["password"];

            if (String.IsNullOrEmpty(password))
            {
                return RedirectToAction("Index", "ShoppingСart");
            }

            
            // Сбои при входе не приводят к блокированию учетной записи
            // Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true

            var result = await SignInManager.PasswordSignInAsync("admin", password, true, shouldLockout: false);
            
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Admin");
                case SignInStatus.Failure:
                default:
                    return RedirectToAction("Index", "ShoppingСart");
                    
            }
        }

              
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "ShoppingСart");
        }
                
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                if (signInManager != null)
                {
                    signInManager.Dispose();
                    signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region private
                

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        
        #endregion
    }
}