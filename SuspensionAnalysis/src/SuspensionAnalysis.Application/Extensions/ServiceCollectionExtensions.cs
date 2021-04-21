using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace SuspensionAnalysis.Application.Extensions
{
    /// <summary>
    /// It contains the extensions to the class ServiceCollection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// This method configures the documentation file for Swagger User Interface.
        /// </summary>
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyTitle = assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
            //string assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(assemblyTitle, new OpenApiInfo
                {
                    Title = assemblyTitle,
                    //Description = assemblyDescription,
                    Version = "v1"
                });

                //var xmlApiPath = Path.Combine(AppContext.BaseDirectory, "IcVibracoes.xml");
                //var xmlDataContractPath = Path.Combine(AppContext.BaseDirectory, "IcVibracoes.DataContracts.xml");
                //options.IncludeXmlComments(xmlApiPath);
                //options.IncludeXmlComments(xmlDataContractPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
