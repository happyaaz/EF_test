using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LostAndFound.Helpers
{
    public class GetSharedVariables
    {
        public static int ReturnInitialNumberOfDevicesPerPage ()
        {
            int initialNumberOfDevicesPerPage_int = 10;
            return initialNumberOfDevicesPerPage_int;
        }
    }
}