using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using SuspensionAnalysis.Application.Extensions;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.Core.Operations.RunAnalysis;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register operations.
            services.AddScoped<ICalculateReactions, CalculateReactions>();
            services.AddScoped<IRunAnalysis<CircularProfile>, RunAnalysis<CircularProfile>>();
            services.AddScoped<IRunAnalysis<RectangularProfile>, RunAnalysis<RectangularProfile>>();

            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerDocs();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocs();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
