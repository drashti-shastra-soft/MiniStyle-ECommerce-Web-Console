using MiniStyleWebConsole.Filter;
using MiniStyleWebConsole.Helper;
using MiniStyleWebConsole.Infrastructure;
using MiniStyleWebConsole.Infrastructure.IService;
using System;
using System.Dynamic;
using System.Web.Mvc;

namespace MiniStyleWebConsole.Controllers
{
    [Authentication]
    public class FabricController : Controller
    {

        private readonly IFabricService _service;
        private readonly IDropdownService _dropdownService;

        public FabricController(IFabricService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.FabricLister());
        }

        public ActionResult List(Domain.FabricLister mLister)
        {
            bool isSuccess = true;
            try
            {
                System.Threading.Thread.Sleep(1000);
                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();
            }
            catch (UnauthorizedAccessException)
            {
                isSuccess = false;
                return PartialView("_LoginPrompt");
            }
            catch (BadRequestException ex)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = ex.Message;
            }
            catch
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Something went wrong!";
            }
            finally
            {
                if (isSuccess)
                {
                    ViewBag.Status = _dropdownService.GetStatusOptions();
                    ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                    mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                }
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create", new Domain.Fabric());
        }

        [HttpPost]
        public ActionResult Create(Domain.Fabric mFabric)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mFabric = StaticMethods.SetAuditOnUpsert(mFabric);
                mFabric = _service.CreateAsync(mFabric).GetAwaiter().GetResult();

                if (mFabric != null && mFabric.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Fabric has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Fabric!";
                }
            }
            catch (UnauthorizedAccessException)
            {
                return PartialView("_LoginPrompt");
            }
            catch (BadRequestException ex)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = ex.Message;
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Status = _dropdownService.GetStatusOptions();
            }

            return PartialView("_Create", mFabric);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mFabric = new Domain.Fabric();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mFabric = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (UnauthorizedAccessException)
            {
                return PartialView("_LoginPrompt");
            }
            catch (BadRequestException ex)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = ex.Message;
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Status = _dropdownService.GetStatusOptions();
            }

            return PartialView("_Create", mFabric);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Fabric has been deleted!" };
            }
            catch (UnauthorizedAccessException)
            {
                resultData = new { StatusCode = "Unauthorized" };
            }
            catch (BadRequestException ex)
            {
                resultData = new { StatusCode = "BadRequest", NotificationType = "error", NotificationMsg = ex.Message };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Fabric!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }


    }
}