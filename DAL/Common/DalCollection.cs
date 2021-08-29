using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using Microsoft.Extensions.DependencyInjection;
using IDAL.User;
using DAL.User;
using IDAL.Firm;
using DAL.Firm;
using IDAL.Common;
using DAL.Common;

namespace DAL.Common
{
   public static class DalCollection
   {
        public static IServiceCollection RegisterDal(this IServiceCollection services)
        {
            services.AddTransient<IDalUser, DalUser>();
            services.AddTransient<IDALFirm, DalFirm>();
            services.AddTransient<IDalCommon, DalCommon>();
            return services;
        }
    }
}
