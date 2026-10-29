using MiniStyleWebConsole.Infrastructure.IService;
using MiniStyleWebConsole.Infrastructure.Service;
using MiniStyleWebConsole.Service;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MiniStyleWebConsole
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IAgeGroupService, AgeGroupService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IChangePasswordService, ChangePasswordService>();
            container.RegisterType<IColorService, ColorService>();
            container.RegisterType<IDropdownService, DropdownService>();
            container.RegisterType<IFabricService, FabricService>();
            container.RegisterType<IForgotPasswordService, ForgotPasswordService>();
            container.RegisterType<IHttpClientService, HttpClientService>();
            container.RegisterType<IMicroCategoryService, MicroCategoryService>();
            container.RegisterType<IOrderLineService, OrderLineService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IProductDetailService, ProductDetailService>();
            container.RegisterType<IProductImageService, ProductImageService>();
            container.RegisterType<IProductPriceService, ProductPriceService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductSizeChartService, ProductSizeChartService>();
            container.RegisterType<ISubCategoryService, SubCategoryService>();
            container.RegisterType<IUserService, UserService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}