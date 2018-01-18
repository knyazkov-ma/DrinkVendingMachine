using System.Web.Mvc;
using DrinkVendingMachine.DataService.Common;
using System.Collections.Generic;
using System;
using log4net;

namespace DrinkVendingMachine.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(" DrinkVendingMachine");
        protected string errorGeneralMessage;        

        protected void setModelStateErrors(DataServiceException ex)
        {

            if (ex.DataServiceExceptionData != null && !String.IsNullOrWhiteSpace(ex.DataServiceExceptionData.GeneralMessage))
            {
                ModelState.AddModelError("ErrorGeneralMessage", ex.DataServiceExceptionData.GeneralMessage);
                errorGeneralMessage = ex.DataServiceExceptionData.GeneralMessage;
            }


            if (ex.DataServiceExceptionData != null && ex.DataServiceExceptionData.Messages != null)
            {
                foreach (string name in ex.DataServiceExceptionData.Messages.Keys)
                {
                    IList<string> list = ex.DataServiceExceptionData.Messages[name];
                    foreach (string msg in list)
                    {
                        ModelState.AddModelError(name, msg);
                    }
                }
            }
            
        }
        
        protected bool execute(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (DataServiceException ex)
            {
                setModelStateErrors(ex);
                return false;

            }
            catch (Exception ex)
            {
                log.Error("Application error", ex);
                return false;
            }
        }
    }
}