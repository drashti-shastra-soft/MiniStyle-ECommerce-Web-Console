using MiniStyleWebConsole.Infrastructure;
using MiniStyleWebConsole.Filter;
using System.Web.Mvc;

namespace MiniStyleWebConsole.Controllers
{
    [Authentication]
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            HttpContextHelper.LoginUser = null;
            return RedirectToAction("Index", "Authentication");
        }
    }
}