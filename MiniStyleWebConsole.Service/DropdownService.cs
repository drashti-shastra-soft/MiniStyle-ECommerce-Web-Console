using MiniStyleWebConsole.Infrastructure;
using MiniStyleWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MiniStyleWebConsole.Service
{
    public class DropdownService : IDropdownService
    {
        #region Declaration

        private readonly IHttpClientService _httpClientService;
        private readonly string baseUrl;

        public DropdownService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }

        #endregion

        #region Category
        public async Task<dynamic> GetCategoriesOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mCategories = await _httpClientService.GetAsync<List<Domain.Category>>(
                url: $"{baseUrl}/Category",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mCategory in mCategories)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mCategory.Name, Value = Convert.ToString(mCategory.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Sub Category
        public async Task<dynamic> GetSubCategoriesOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mSubCategories = await _httpClientService.GetAsync<List<Domain.SubCategory>>(
                url: $"{baseUrl}/SubCategory",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mSubCategory in mSubCategories)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mSubCategory.Name, Value = Convert.ToString(mSubCategory.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Micro Category
        public async Task<dynamic> GetMicroCategoriesOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mMicroCategories = await _httpClientService.GetAsync<List<Domain.Product>>(
                url: $"{baseUrl}/MicroCategory",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mMicroCategory in mMicroCategories)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mMicroCategory.Name, Value = Convert.ToString(mMicroCategory.Id) });
            }
            return mSelectListItems;
        }

        #endregion

        #region Fabric
        public async Task<dynamic> GetFabricOptions()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mFabrics = await _httpClientService.GetAsync<List<Domain.Fabric>>(
                url: $"{baseUrl}/Fabric/GetAllActiveFabric",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mFabric in mFabrics)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mFabric.Name, Value = Convert.ToString(mFabric.Id) });
            }

            return mSelectListItems;
        }
        #endregion

        #region Pagination
        public dynamic GetPageSizeOptions(int selectedSize = 20)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "All", Value = "-1" }
            };

            list.ForEach(item => item.Selected = item.Value == selectedSize.ToString());
            return list;
        }
        #endregion

        #region Status
        public dynamic GetStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Active", Value = "Active" },
                new SelectListItem { Text = "Disabled", Value = "Disabled" },
            };
        }
        #endregion

        #region Product Color
        public async Task<dynamic> GetProductColorOptions(int productId)
        {
            var mSelectListItems = new List<SelectListItem>();

            var mProductColors = await _httpClientService.GetAsync<List<Domain.ProductColor>>(
                url: $"{baseUrl}/ProductColor/ProductId/{productId}",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mProductColor in mProductColors)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mProductColor.Color, Value = Convert.ToString(mProductColor.Id) });
            }

            return mSelectListItems;
        }

        #endregion

        #region Product Age group

        public async Task<dynamic> GetProductAgeGroupOptions(int productId)
        {
            var mSelectListItems = new List<SelectListItem>();

            var mProductAgeGroups = await _httpClientService.GetAsync<List<Domain.ProductAgeGroup>>(
                url: $"{baseUrl}/ProductAgeGroup/ProductId/{productId}",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mProductAgeGroup in mProductAgeGroups)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mProductAgeGroup.AgeGroup, Value = Convert.ToString(mProductAgeGroup.Id) });
            }

            return mSelectListItems;
        }
        #endregion

        #region Shipping Status

        public dynamic GetShippingStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Not Shipped", Value = "Not Shipped" },
                new SelectListItem { Text = "Processing", Value = "Processing" },
                new SelectListItem { Text = "Shipped", Value = "Shipped" },
                new SelectListItem { Text = "In Transit", Value = "In Transit" },
                new SelectListItem { Text = "Out for Delivery", Value = "Out for Delivery" },
                new SelectListItem { Text = "Delivered", Value = "Delivered" },
                new SelectListItem { Text = "Returned", Value = "Returned" },
                new SelectListItem { Text = "Cancelled", Value = "Cancelled" },
                new SelectListItem { Text = "Failed Delivery", Value = "Failed Delivery" },
                new SelectListItem { Text = "Awaiting Pickup", Value = "Awaiting Pickup" },
            };
        }

        #endregion

        #region Payment Status

        public dynamic GetPaymentStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Pending", Value = "Pending" },
                new SelectListItem { Text = "Completed", Value = "Completed" },
                new SelectListItem { Text = "Failed", Value = "Failed" },
                new SelectListItem { Text = "Refunded", Value = "Refunded" },
                new SelectListItem { Text = "Partially Refunded", Value = "Partially Refunded" },
                new SelectListItem { Text = "Cancelled", Value = "Cancelled" },
                new SelectListItem { Text = "Authorized", Value = "Authorized" },
                new SelectListItem { Text = "Captured", Value = "Captured" },
                new SelectListItem { Text = "Charge back", Value = "Charge back" },
                new SelectListItem { Text = "On Hold", Value = "On Hold" },
            };
        }

        #endregion

        #region Wearable Part
        public dynamic GetWearablePartOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Top", Value = "Top" },
                new SelectListItem { Text = "Bottom", Value = "Bottom" },
            };
        }

        #endregion


    }
}
