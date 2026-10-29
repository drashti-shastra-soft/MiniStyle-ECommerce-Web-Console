using MiniStyleWebConsole.Infrastructure.IService;
using MiniStyleWebConsole.Infrastructure;
using MiniStyleWebConsole.Filter;
using MiniStyleWebConsole.Helper;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System;


namespace MiniStyleWebConsole.Controllers
{
    [Authentication]
    public class ProductController : Controller
    {
        #region Declaration 

        private readonly IProductService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IAgeGroupService _ageGroupService;
        private readonly IColorService _colorService;
        private readonly IProductDetailService _productDetailService;
        private readonly IProductPriceService _productpriceService;
        private readonly IProductImageService _productImageService;
        private readonly IProductSizeChartService _productSizeChartService;

        public ProductController(IProductService service, IDropdownService dropdownService, IAgeGroupService ageGroupService, IColorService colorService, IProductDetailService productDetailService, IProductImageService productImageService, IProductPriceService productpriceService, IProductSizeChartService productSizeChartService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _ageGroupService = ageGroupService;
            _colorService = colorService;
            _productDetailService = productDetailService;
            _productImageService = productImageService;
            _productpriceService = productpriceService;
            _productSizeChartService = productSizeChartService;
        }

        #endregion

        #region Public Method
        public ActionResult Index()
        {
            return View(new Domain.ProductLister());
        }

        public ActionResult List(Domain.ProductLister mLister)
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
                    ViewBag.SubCategory = _dropdownService.GetSubCategoriesOptionsAsync().GetAwaiter().GetResult();
                    ViewBag.MicroCategory = _dropdownService.GetMicroCategoriesOptionsAsync().GetAwaiter().GetResult();
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
                ViewBag.MicroCategory = _dropdownService.GetMicroCategoriesOptionsAsync().GetAwaiter().GetResult();
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
            return PartialView("_Create", new Domain.Product());
        }

        [HttpPost]
        public ActionResult Create(Domain.Product mProduct)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProduct = StaticMethods.SetAuditOnUpsert(mProduct);
                mProduct = _service.CreateAsync(mProduct).GetAwaiter().GetResult();
                if (mProduct != null && mProduct.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Product has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving product!";
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
                ViewBag.SubCategory = _dropdownService.GetSubCategoriesOptionsAsync().GetAwaiter().GetResult();
                ViewBag.MicroCategory = _dropdownService.GetMicroCategoriesOptionsAsync().GetAwaiter().GetResult();
            }

            return PartialView("_Create", mProduct);
        }

        [HttpGet]
        public JsonResult GetProductMetadata(int id)
        {
            string redirectUrl = Url.Action("Detail", "Product", new { id = id });
            return Json(new { success = true, redirectUrl }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Product has been deleted!" };
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
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Product!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Product

        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            var mProduct = new Domain.Product();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProduct = _service.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.MicroCategory = _dropdownService.GetMicroCategoriesOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Product", mProduct);
        }

        [HttpPost, Route("SaveProduct")]
        public ActionResult SaveProduct(Domain.Product mProduct)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProduct = _service.CreateAsync(mProduct).GetAwaiter().GetResult();
                if (mProduct != null && mProduct.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Product has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving product!";
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
                ViewBag.SubCategory = _dropdownService.GetSubCategoriesOptionsAsync().GetAwaiter().GetResult();
                ViewBag.MicroCategory = _dropdownService.GetMicroCategoriesOptionsAsync().GetAwaiter().GetResult();
            }

            return PartialView("_Product", mProduct);
        }

        #endregion

        #region Product Detail
        [HttpGet]
        public ActionResult GetProductDetail(int id)
        {
            var mProductDetail = new Domain.ProductDetail();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProductDetail = _productDetailService.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.Fabric = _dropdownService.GetFabricOptions().GetAwaiter().GetResult();
            }
            return PartialView("_ProductDetail", mProductDetail);
        }

        [HttpPost, Route("UpsertProductDetail")]
        public ActionResult UpsertProductDetail(Domain.ProductDetail mProductDetail)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProductDetail = _productDetailService.UpsertAsync(mProductDetail).GetAwaiter().GetResult();
                if (mProductDetail != null && mProductDetail.ProductDetailId > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Product Detail has been saved Successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving product detail!";
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
                ViewBag.Fabric = _dropdownService.GetFabricOptions().GetAwaiter().GetResult();
            }
            return PartialView("_ProductDetail", mProductDetail);
        }

        #endregion

        #region Product Price
        [HttpGet]
        public ActionResult GetProductPrice(int id)
        {
            var mProductPrice = new Domain.ProductPrice();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProductPrice = _productpriceService.GetProductPrice(id).GetAwaiter().GetResult();
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

            return PartialView("_ProductPrice", mProductPrice);
        }

        [HttpPost, Route("SaveProductPrice")]
        public ActionResult SaveProductPrice(Domain.ProductPrice mProductPrice)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProductPrice = _productpriceService.SaveProductPrice(mProductPrice).GetAwaiter().GetResult();
                if (mProductPrice != null && mProductPrice.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Product Price has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving product price!";
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
            return PartialView("_ProductPrice", mProductPrice);
        }

        #endregion

        #region Size Chart

        [HttpGet]
        public ActionResult GetProductSizeChart(int id)
        {
            ViewBag.ProductId = id;
            return PartialView("_ProductSizeChart");
        }

        [HttpGet]
        public ActionResult GetProductSizeChartList(int id, string unitType)
        {
            var mSizeChartRows = new List<Domain.ProductSizeChart>();
            try
            {
                System.Threading.Thread.Sleep(1000);                
                mSizeChartRows = _productSizeChartService.Get(id, unitType).GetAwaiter().GetResult();
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
                ViewBag.WearablePart = _dropdownService.GetWearablePartOptions();
            }

            return PartialView("_ProductSizeChartList", mSizeChartRows); 
        }

        [HttpGet]
        public ActionResult CreateProductSizeChart(int productId)
        {
            try
            {
                ViewBag.AgeGroup = new SelectList(_dropdownService.GetProductAgeGroupOptions(productId).GetAwaiter().GetResult(), "Value", "Text");
                ViewBag.WearablePart = _dropdownService.GetWearablePartOptions();
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
            return PartialView("_CreateSizeChart", new Domain.ProductSizeChart());
        }

        [HttpPost]
        public ActionResult UpsertProductSizeChart(Domain.ProductSizeChart mProductSizeChart)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mProductSizeChart.CreatedBy = HttpContextHelper.LoginUser.Id;
                mProductSizeChart.LastModifiedBy = HttpContextHelper.LoginUser.Id;
                mProductSizeChart = _productSizeChartService.Upsert(mProductSizeChart).GetAwaiter().GetResult();
                ViewBag.AgeGroup = _dropdownService.GetProductAgeGroupOptions(mProductSizeChart.ProductId).GetAwaiter().GetResult();
                ViewBag.WearablePart = _dropdownService.GetWearablePartOptions();

                if (mProductSizeChart != null && mProductSizeChart.Id > 0)
                {
                    resultData = new { type = "success", message = "Size Chart has been saved successfully!" };
                }
                else
                {
                    resultData = new { type = "error", message = "Error occurred while saving Size Chart" };
                }
            }
            catch (UnauthorizedAccessException)
            {
                return PartialView("_LoginPrompt");
            }
            catch (BadRequestException ex)
            {
                resultData = new { type = "success", message = ex.Message };
            }
            catch (Exception)
            {
                resultData = new { type = "error", message = "Internal server error!" };
            }            
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Age Group

        [HttpGet]
        public ActionResult GetAgeGroup(int id)
        {
            var mAgeGroups = new List<Domain.AgeGroup>();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mAgeGroups = _ageGroupService.GetAllActiveAgeGroup().GetAwaiter().GetResult();
                var mProductAgeGroups = _ageGroupService.GetProductAgeGroup(id).GetAwaiter().GetResult();
                if (mAgeGroups != null && mAgeGroups.Count > 0)
                {
                    if (mProductAgeGroups != null && mProductAgeGroups.Count > 0)
                    {
                        var selectedAgeGroups = new HashSet<int>(mProductAgeGroups.Select(x => x.AgeGroupId));
                        foreach (var item in mAgeGroups)
                        {
                            item.IsChecked = selectedAgeGroups.Contains(item.Id);
                        }
                    }
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
            return PartialView("_AgeGroup", mAgeGroups);
        }

        [HttpPost, Route("SaveProductAgeGroup")]
        public ActionResult SaveProductAgeGroup(List<Domain.AgeGroup> mAgeGroups, int productId)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                var mProductAgeGroups = new List<Domain.ProductAgeGroup>();

                foreach (var item in mAgeGroups.Where(x => x.IsChecked))
                {
                    var mProductAgeGroup = new Domain.ProductAgeGroup();
                    mProductAgeGroup.AgeGroupId = item.Id;
                    mProductAgeGroup.ProductId = productId;
                    mProductAgeGroups.Add(mProductAgeGroup);
                }

                _ageGroupService.SaveProductAgeGroup(mProductAgeGroups).GetAwaiter().GetResult();
                ViewBag.NotificationType = "success";
                ViewBag.NotificationMsg = "Age Group has been saved Successfully!";
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
            catch
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_AgeGroup", mAgeGroups);
        }

        #endregion

        #region Color

        [HttpGet]
        public ActionResult GetColor(int id)
        {
            var mColors = new List<Domain.Color>();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mColors = _colorService.GetAllActiveColor().GetAwaiter().GetResult();
                var mProductColor = _colorService.GetProductColor(id).GetAwaiter().GetResult();
                if (mColors != null && mColors.Count > 0)
                {
                    if (mProductColor != null && mProductColor.Count > 0)
                    {
                        var selectedColor = new HashSet<int>(mProductColor.Select(x => x.ColorId));
                        foreach (var item in mColors)
                        {
                            item.IsChecked = selectedColor.Contains(item.Id);
                        }
                    }
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

            return PartialView("_Color", mColors);
        }

        [HttpPost, Route("SaveProductColor")]
        public ActionResult SaveProductColor(List<Domain.Color> mColors, int productId)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                var mProductColors = new List<Domain.ProductColor>();

                foreach (var item in mColors.Where(x => x.IsChecked))
                {
                    var mProductColor = new Domain.ProductColor();
                    mProductColor.ColorId = item.Id;
                    mProductColor.ProductId = productId;
                    mProductColors.Add(mProductColor);
                }

                _colorService.SaveProductColor(mProductColors).GetAwaiter().GetResult();
                ViewBag.NotificationType = "success";
                ViewBag.NotificationMsg = "Color has been saved Successfully!";
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
            return PartialView("_Color", mColors);
        }

        #endregion

        #region Product Images

        [HttpGet, Route("GetProductImages")]
        public ActionResult GetProductImages(int id)
        {
            var mProductImageVM = new Domain.ProductImageViewModel();
            try
            {
                mProductImageVM.ProductImages = _productImageService.GetProductImages(id).GetAwaiter().GetResult();
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
                ViewBag.ProductColorId = _dropdownService.GetProductColorOptions(id).GetAwaiter().GetResult();
            }
            return PartialView("_ProductImage", mProductImageVM);
        }

        [HttpPost, Route("SaveProductImage")]
        public ActionResult SaveProductImage(int productId, int productColorId, HttpPostedFileBase file)
        {
            var mProductImages = new List<Domain.ProductImage>();
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (file != null && file.ContentLength > 0)
                {
                    string base64String = string.Empty;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.InputStream.CopyTo(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();

                        // Convert the byte array to a Base64 string.
                        base64String = Convert.ToBase64String(fileBytes);
                    }

                    var mProductImage = new Domain.ProductImage()
                    {
                        ProductId = productId,
                        ProductColorId = productColorId,
                        FileName = file.FileName,
                        MimeType = file.ContentType,
                        Base64String = base64String
                    };
                    mProductImage = _productImageService.SaveProductImage(mProductImage).GetAwaiter().GetResult();
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
            catch
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            finally
            {
                ViewBag.ProductColorId = _dropdownService.GetProductColorOptions(productId).GetAwaiter().GetResult();
            }
            return Json(new { success = true });
        }

        [HttpGet, Route("FetchProductImages")]
        public ActionResult FetchProductImages(int id, int productColorId)
        {
            dynamic jsonResult = new ExpandoObject();
            try
            {
                var mProductImages = _productImageService.GetProductImages(id, productColorId).GetAwaiter().GetResult();

                jsonResult = Json(mProductImages.Select(img => new
                {
                    Id = img.Id,
                    FileName = img.FileName,
                    MimeType = img.MimeType,
                    Base64String = img.Base64String
                }), JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue; // 🚀 This is the key line
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
                jsonResult = new { NotificationType = "error", NotificationMsg = "Error occurred while get product images!" };
            }
            return jsonResult;
        }

        [HttpGet, Route("DeleteProductImage")]
        public ActionResult DeleteProductImage(int id)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                _productImageService.DeleteProductImage(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Product Image has been deleted!" };
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
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Product Image" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}