using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dentis.Utils
{
    public class Utils
    {
        public static bool IsSuperUser(int securityUserTypeId)
        {
            if (securityUserTypeId == 1)
            {
                return true; 
            }

            return false;
        }
    }
}
