using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure
{
    public class EnumsHelper
    {
        public static string GetEnumString(Enums.Operation operation)
        {

            switch (operation)
            {
                case Enums.Operation.Save:
                    return nameof(Enums.Operation.Save);
                case Enums.Operation.Delete:
                    return nameof(Enums.Operation.Delete);
                default:
                    return null;
            }
        }

        public static string GetEnumString(Enums.MenuItem menuItem)
        {

            switch (menuItem)
            {
                case Enums.MenuItem.Company:
                    return nameof(Enums.MenuItem.Company);
                case Enums.MenuItem.User:
                    return nameof(Enums.MenuItem.User);
                default:
                    return null;
            }
        }

        public static string GetEnumString(Enums.Result result)
        {
            switch (result)
            {
                case Enums.Result.Success:
                    return nameof(Enums.Result.Success);
                case Enums.Result.Error:
                    return nameof(Enums.Result.Error);
                default:
                    return null;
            }
        }

        public static string GetEnumString(Enums.WearablePart result)
        {
            switch (result)
            {
                case Enums.WearablePart.Top:
                    return nameof(Enums.WearablePart.Top);
                case Enums.WearablePart.Bottom:
                    return nameof(Enums.WearablePart.Bottom);
                default:
                    return null;
            }
        }

        //public static string GetEnumString(Enums.ShippingStatus shippingStatus)
        //{
        //    switch (shippingStatus)
        //    {
        //        case Enums.ShippingStatus.Not_Shipped:
        //            return nameof(Enums.ShippingStatus.Not_Shipped);
        //        case Enums.ShippingStatus.Processing:
        //            return nameof(Enums.ShippingStatus.Processing);
        //        case Enums.ShippingStatus.Shipped:
        //            return nameof(Enums.ShippingStatus.Shipped);
        //        case Enums.ShippingStatus.In_Transit:
        //            return nameof(Enums.ShippingStatus.In_Transit);
        //        case Enums.ShippingStatus.Out_for_Delivery:
        //            return nameof(Enums.ShippingStatus.Out_for_Delivery);
        //        case Enums.ShippingStatus.Delivered:
        //            return nameof(Enums.ShippingStatus.Delivered);
        //        case Enums.ShippingStatus.Returned:
        //            return nameof(Enums.ShippingStatus.Returned);
        //        case Enums.ShippingStatus.Cancelled:
        //            return nameof(Enums.ShippingStatus.Cancelled);
        //        case Enums.ShippingStatus.Failed_Delivery:
        //            return nameof(Enums.ShippingStatus.Failed_Delivery);
        //        case Enums.ShippingStatus.Awaiting_Pickup:
        //            return nameof(Enums.ShippingStatus.Awaiting_Pickup);

        //        default:
        //            return null;
        //    }
        //}


    }
}
