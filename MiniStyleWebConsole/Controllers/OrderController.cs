using MiniStyleWebConsole.Filter;
using MiniStyleWebConsole.Helper;
using MiniStyleWebConsole.Infrastructure;
using MiniStyleWebConsole.Infrastructure.IService;
using MiniStyleWebConsole.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;

namespace MiniStyleWebConsole.Controllers
{
    [Authentication]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IOrderLineService _orderLineService;

        public OrderController(IOrderService service, IOrderLineService orderLineService, IDropdownService dropdownService)
        {
            _service = service;
            _orderLineService = orderLineService;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.OrderLister());
        }

        public ActionResult List(Domain.OrderLister mLister)
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
                    ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                    mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                }
            }
            return PartialView("_List", mLister);
        }

        [HttpPost]
        public ActionResult Create(Domain.Order mOrder)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mOrder = _service.CreateAsync(mOrder).GetAwaiter().GetResult();

                if (mOrder != null && mOrder.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Order has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Order!";
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
                ViewBag.ShippingStatus = _dropdownService.GetShippingStatusOptions();
                ViewBag.PaymentStatus = _dropdownService.GetPaymentStatusOptions();
            }

            return PartialView("_Create", mOrder);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mOrder = new Domain.Order();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mOrder = _service.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.ShippingStatus = _dropdownService.GetShippingStatusOptions();
                ViewBag.PaymentStatus = _dropdownService.GetPaymentStatusOptions();
            }

            return PartialView("_Create", mOrder);
        }

        [HttpGet, Route("GetOrderLine")]
        public ActionResult GetOrderLine(int id)
        {
            var mOrderLine = new List<Domain.OrderLine>();
            try
            {
                Thread.Sleep(1000);
                mOrderLine = _orderLineService.GetByAsync(id).GetAwaiter().GetResult();
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
                ViewBag.NotificationMsg = "Internal Server Error";
            }
            return PartialView("_OrderLine", mOrderLine);
        }

    }
}