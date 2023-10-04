// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     LoginControllerRoute.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   22:31
// 
// Last changed: 2015-01-06   22:33
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace TMS.be_together.Constants
{
    public static class AccountControllerRoute
    {
        public const string GetRegister = ControllerName.Account + "GetRegister";
        public const string GetLogin = ControllerName.Account + "GetLogin";
        public const string GetForgotPassword = ControllerName.Account + "GetForgotPassword";
        public const string GetExternalLoginFailure = ControllerName.Account + "GetExternalLoginFailure";
        public const string GetForgotPasswordConfirmation = ControllerName.Account + "GetForgotPasswordConfirmation";
        public const string GetLogOff = ControllerName.Account + "GetLogOff";
    }
}