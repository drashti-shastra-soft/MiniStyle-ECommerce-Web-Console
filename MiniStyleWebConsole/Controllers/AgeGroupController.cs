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
    public class AgeGroupController : Controller
    {
        private readonly IAgeGroupService _service;
        private readonly IDropdownService _dropdownService;

        public AgeGroupController(IAgeGroupService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.AgeGroupLister());
        }

        public ActionResult List(Domain.AgeGroupLister mLister)
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
            catch(Exception)
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
            return PartialView("_Create", new Domain.AgeGroup());
        }

        [HttpPost]
        public ActionResult Create(Domain.AgeGroup mAgeGroup)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mAgeGroup = StaticMethods.SetAuditOnUpsert(mAgeGroup);
                mAgeGroup = _service.CreateAsync(mAgeGroup).GetAwaiter().GetResult();

                if (mAgeGroup != null && mAgeGroup.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Age has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Age!";
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

            return PartialView("_Create", mAgeGroup);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mAgeGroup = new Domain.AgeGroup();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mAgeGroup = _service.GetAsync(id).GetAwaiter().GetResult();
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

            return PartialView("_Create", mAgeGroup);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Age has been deleted!" };
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
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Age!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
    }
}