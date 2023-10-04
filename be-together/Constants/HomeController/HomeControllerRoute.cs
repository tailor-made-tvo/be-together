// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     HomeControllerRoute.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:03
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace TMS.be_together.Constants
{
    public static class HomeControllerRoute
    {
        public const string GetAbout = ControllerName.Home + "GetAbout";
        public const string GetContact = ControllerName.Home + "GetContact";
        public const string GetIndex = ControllerName.Home + "GetIndex";
        public const string RobotsText = ControllerName.Home + "RobotsText";
        public const string SitemapXml = ControllerName.Home + "SitemapXml";
        //public const string GetLoginPartial = ControllerName.Shared + "_LoginPartial";
    }
}