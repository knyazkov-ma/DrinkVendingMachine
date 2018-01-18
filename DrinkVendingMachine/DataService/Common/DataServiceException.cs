using System;
using System.Collections.Generic;

namespace DrinkVendingMachine.DataService.Common
{
    public class DataServiceException : Exception
    {
        public DataServiceInfo DataServiceExceptionData { get; private set; }
        
        public DataServiceException()
            : base()
        { }

        public DataServiceException(string message)
            : base(message)
        {
            DataServiceExceptionData = new DataServiceInfo();
            DataServiceExceptionData.GeneralMessage = message;
        }
        
        public DataServiceException(string message, Dictionary<string, List<string>> messages)
            : base(message)
        {
            DataServiceExceptionData = new DataServiceInfo();

            DataServiceExceptionData.GeneralMessage = message;
            DataServiceExceptionData.Messages = messages;
        }        

    }
}
