using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZSZ.CommonMVC
{
    public class TrimToDBCModelBinder:DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object value = base.BindModel(controllerContext, bindingContext);
            
            if(value is string)
            {
                string strValue = (string)value;
                strValue = ToDBC(strValue).Trim();
                return strValue;
            }
            else
            {
                return value;
            }
        }

        private static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if(c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if(c[i] > 65280 && c[i] < 65375)
                {
                    c[i] =(char)(c[i] - 65248);
                }
            }
            return new string(c);
        }
    }
}
