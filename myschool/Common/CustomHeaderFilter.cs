using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschool.Common
{
    public class CustomHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "auth",
                In = ParameterLocation.Header,
                Description = "access token",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                    //Default = new OpenApiString("")
                }
            });
            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "device",
            //    In = ParameterLocation.Header,
            //    Description = "login device type.",
            //    Required = false,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "String",
            //        Default = new OpenApiString("")
            //    }
            //});
            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "ip",
            //    In = ParameterLocation.Header,
            //    Description = "login device ip.",
            //    Required = false,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "String",
            //        Default = new OpenApiString("")
            //    }
            //});

            //if (isAuthorized && !allowAnonymous)
            //{
            //    if (operation.Parameters == null)
            //        operation.Parameters = new List<OpenApiParameter>();


            //}
        }
    }
}
