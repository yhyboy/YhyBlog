using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using log4net;
using WebPage.Other;
using System.Web.Http;
using AutoMapper;
using WebPage.Odt;

namespace WebPage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MefInit();
            AutoMaperInit();
        }

        /// <summary>
        /// MEF初始化
        /// </summary>
        private void MefInit()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
            MefDependencySolver solver = new MefDependencySolver(catalog);
            DependencyResolver.SetResolver(solver);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = solver;
        }


        private void AutoMaperInit()
        {
            Configuration.Configure();
            //在程序启动时对所有的配置进行严格的验证  
           // Mapper.AssertConfigurationIsValid(); 
        }
    }
}
