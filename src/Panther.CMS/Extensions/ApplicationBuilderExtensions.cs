﻿using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;

namespace Panther.CMS.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds a StatusCodePages middleware to the pipeline. Specifies that the response body should be generated by 
        /// re-executing the request pipeline using an alternate path. This path may contain a '{0}' placeholder of the status code.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pathFormat"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusNamePagesWithReExecute(this IApplicationBuilder app, string pathFormat)
        {
            return app.UseStatusCodePages(
                async (context) =>
                {
                    int statusCode = context.HttpContext.Response.StatusCode;
                    var status = (HttpStatusCode)context.HttpContext.Response.StatusCode;
                    var newPath = new PathString(string.Format(
                        CultureInfo.InvariantCulture,
                        pathFormat,
                        statusCode,
                        status.ToString()));

                    var originalPath = context.HttpContext.Request.Path;
                    // Store the original paths so the application can check it.
                    context.HttpContext.SetFeature<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
                    {
                        OriginalPathBase = context.HttpContext.Request.PathBase.Value,
                        OriginalPath = originalPath.Value,
                    });

                    context.HttpContext.Request.Path = newPath;
                    try
                    {
                        await context.Next(context.HttpContext);
                    }
                    catch(Exception ex)
                    {

                    }
                    finally
                    {
                        context.HttpContext.Request.Path = originalPath;
                        context.HttpContext.SetFeature<IStatusCodeReExecuteFeature>(null);
                    }
                });
        }
    }
}
