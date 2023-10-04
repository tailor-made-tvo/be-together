// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     Startup.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:04
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

using System.Web.Mvc;
using Owin;

[assembly: Microsoft.Owin.OwinStartup(typeof (TMS.be_together.Startup))]

namespace TMS.be_together
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureContainer(app);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}