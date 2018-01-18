using System.Collections.Generic;

namespace DrinkVendingMachine.DataService.Common
{
    public class DataServiceInfo
    {
        public Dictionary<string, List<string>> Messages { get; set; }
        public string GeneralMessage { get; set; }
    }
}
