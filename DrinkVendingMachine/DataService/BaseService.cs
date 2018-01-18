using System.Collections.Generic;
using System.Linq;


namespace DrinkVendingMachine.DataService
{
    public abstract class BaseService
    {
        protected Dictionary<string, List<string>> errorMessages = new Dictionary<string, List<string>>();
               

        public void setErrorMessages(string key, string msg)
        {
            if (!errorMessages.Keys.Contains(key))
                errorMessages[key] = new List<string>();
            errorMessages[key].Add(msg);
        }       

    }
}
