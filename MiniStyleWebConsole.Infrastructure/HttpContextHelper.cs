using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MiniStyleWebConsole.Infrastructure
{
    public class HttpContextHelper
    {
        private const string LoginUserKey = "LoginUser";

        public static Domain.User LoginUser
        {
            get
            {
                var context = HttpContext.Current;
                if (context?.Session == null)
                    return null;

                return context.Session[LoginUserKey] as Domain.User;
            }
            set
            {
                var context = HttpContext.Current;
                if (context?.Session != null)
                    context.Session[LoginUserKey] = value;
            }

        }
    }
}
