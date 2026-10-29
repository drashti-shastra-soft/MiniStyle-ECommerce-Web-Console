using System;
using System.Threading.Tasks;


namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IForgotPasswordService
    {
        Task Initiate(Domain.ForgotPassword mForgotPassword);
    }
}
