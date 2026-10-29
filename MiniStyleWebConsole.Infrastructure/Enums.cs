using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure
{
    public class Enums
    {
        public enum Operation
        {
            Save = 1,
            Delete = 2,
        }

        public enum MenuItem
        {
            Company = 1,
            User = 2,
        }

        public enum Result
        {
            Success = 1,
            Error = 2
        }

        public enum WearablePart
        {
            Top = 1,
            Bottom = 2
        }




        //public enum ShippingStatus
        //{
        //    Not_Shipped = 1,
        //    Processing = 2,
        //    Shipped = 3,
        //    In_Transit = 4,
        //    Out_for_Delivery = 5,
        //    Delivered = 6,
        //    Returned = 7,
        //    Cancelled = 8,
        //    Failed_Delivery = 9,
        //    Awaiting_Pickup = 10
        //}

    }
}
