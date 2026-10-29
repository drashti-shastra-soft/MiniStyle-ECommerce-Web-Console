using MiniStyleWebConsole.Domain;
using System;
using System.Linq;

namespace MiniStyleWebConsole.Infrastructure
{
    public static class StaticMethods
    {
        public static T SetAuditOnUpsert<T>(T obj) where T : IAuditable
        {
            obj.CreatedBy = HttpContextHelper.LoginUser.Id;
            obj.LastModifiedBy = HttpContextHelper.LoginUser.Id;
            return obj;
        }
    }
}
