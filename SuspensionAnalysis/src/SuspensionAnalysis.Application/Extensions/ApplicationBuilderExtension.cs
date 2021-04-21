using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace SuspensionAnalysis.Application.Extensions
{
    /// <summary>
    /// It contains the extensions to the class ApplicationBuilder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds Swagger documentations to ApplicationBuilder.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>The Swagger documentations.</returns>
        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
        {
            string assemblyTitle = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{assemblyTitle}/swagger.json", $"{assemblyTitle} API");
                c.EnableValidator(null);
            });

            return app;
        }
    }
}
