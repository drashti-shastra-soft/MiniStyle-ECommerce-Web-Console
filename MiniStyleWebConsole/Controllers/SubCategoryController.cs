using MiniStyleWebConsole.Infrastructure.IService;
using MiniStyleWebConsole.Infrastructure;
using MiniStyleWebConsole.Filter;
using MiniStyleWebConsole.Helper;
using System.Dynamic;
using System.Web.Mvc;
using System;

namespace MiniStyleWebConsole.Controllers
{
    [Authentication]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _service;
        private readonly IDropdownService _dropdownService;

        public SubCategoryController(ISubCategoryService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.SubCategoryLister());
        }

        public ActionResult List(Domain.SubCategoryLister mLister)
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
            catch (Exception)
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
                    ViewBag.Categories = _dropdownService.GetCategoriesOptionsAsync().GetAwaiter().GetResult();
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
                ViewBag.Category = _dropdownService.GetCategoriesOptionsAsync().GetAwaiter().GetResult();
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
            return PartialView("_Create", new Domain.SubCategory());
        }

        [HttpPost]
        public ActionResult Create(Domain.SubCategory mSubCategory)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mSubCategory = StaticMethods.SetAuditOnUpsert(mSubCategory);
                mSubCategory = _service.CreateAsync(mSubCategory).GetAwaiter().GetResult();

                if (mSubCategory != null && mSubCategory.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Sub Category has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Sub Category!";
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
                ViewBag.Category = _dropdownService.GetCategoriesOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mSubCategory);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mSubCategory = new Domain.SubCategory();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mSubCategory = _service.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.Category = _dropdownService.GetCategoriesOptionsAsync().GetAwaiter().GetResult();
            }

            return PartialView("_Create", mSubCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { StatusCode = "Ok", NotificationType = "success", NotificationMsg = "Sub Category has been deleted!" };
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
                resultData = new { StatusCode = "Error", NotificationType = "error", NotificationMsg = "Error occurred while deleting Sub Category!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

    }
}