using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using IBAL;
using IBAL.User;
using BAL.User;
using IBAL.Firm;
using BAL.Firm;
using IBAL.Common;
using BAL.Common;


namespace BAL.Common
{
    public static class ServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IBalUser, BalUser>();
            services.AddTransient<IBalFirm, BalFirm>();
            services.AddTransient<IBALCommon, BALCommon>();
            return services;
        }
    }
}
