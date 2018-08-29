using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZSZ.CommonMVC
{
    public class MVCHelper
    {
        public static string GetValidMsg(ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ms in modelState.Values)
            {
                foreach (var modelError in ms.Errors)
                {
                    sb.AppendLine(modelError.ErrorMessage);
                }
            }
            return sb.ToString();
        }
    }
}
