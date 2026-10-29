using MiniStyleWebConsole.Infrastructure.IService;
using MiniStyleWebConsole.Infrastructure;
using MiniStyleWebConsole.Filter;
using MiniStyleWebConsole.Helper;
using System.Web.Mvc;
using System.Dynamic;
using System;

namespace MiniStyleWebConsole.Controllers
{
    [Authentication]
    public class MicroCategoryController : Controller
    {
        private readonly IMicroCategoryService _service;
        private readonly IDropdownService _dropdownService;

        public MicroCategoryController(IMicroCategoryService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.MicroCategoryLister());
        }

        public ActionResult List(Domain.MicroCategoryLister mLister)
        {
            bool isSuccess = true;
            try
            {
                System.Threading.Thread.Sleep(1000);
                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();
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
                    ViewBag.SubCategory = _dropdownService.GetSubCategoriesOptionsAsync().GetAwaiter().GetResult();
                    mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                }
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.SubCategory = _dropdownService.GetSubCategoriesOptionsAsync().GetAwaiter().GetResult();
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
                ViewBag.NotificationMsg = "Something went wrong!";
            }

            return PartialView("_Create", new Domain.MicroCategory());
        }

        [HttpPost]
        public ActionResult Create(Domain.MicroCategory mMicroCategory)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mMicroCategory = StaticMethods.SetAuditOnUpsert(mMicroCategory);
                mMicroCategory = _service.CreateAsync(mMicroCategory).GetAwaiter().GetResult();

                if (mMicroCategory != null && mMicroCategory.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Micro Category has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Micro Category!";
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
                ViewBag.SubCategory = _dropdownService.GetSubCategoriesOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mMicroCategory);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mMicroCategory = new Domain.MicroCategory();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mMicroCategory = _service.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.SubCategory = _dropdownService.GetSubCategoriesOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Status = _dropdownService.GetStatusOptions();
            }

            return PartialView("_Create", mMicroCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Micro Category has been deleted!" };
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
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Micro Category!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
    }
}