using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using SuspensionAnalysis.Application.Extensions;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile;
using SuspensionAnalysis.Core.GeometricProperties.CircularProfile;
using SuspensionAnalysis.Core.GeometricProperties.RectangularProfile;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation.Newmark;
using SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation.NewmarkBeta;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.Core.Operations.RunAnalysis.Dynamic.HalfCar;
using SuspensionAnalysis.Core.Operations.RunAnalysis.Static.CircularProfile;
using SuspensionAnalysis.Core.Operations.RunAnalysis.Static.RectangularProfile;

namespace SuspensionAnalysis
{
    /// <summary>
    /// The application startup.
    /// It configures the dependency injection and adds all necessary configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Constitutive Equations
            services.AddScoped<ICircularProfileMechanicsOfMaterials, CircularProfileMechanicsOfMaterials>();
            services.AddScoped<IRectangularProfileMechanicsOfMaterials, RectangularProfileMechanicsOfMaterials>();

            // Register Geometric Property calculators.
            services.AddScoped<ICircularProfileGeometricProperty, CircularProfileGeometricProperty>();
            services.AddScoped<IRectangularProfileGeometricProperty, RectangularProfileGeometricProperty>();

            // Register Mapper
            services.AddScoped<IMappingResolver, MappingResolver>();

            // Register operations.
            services.AddScoped<ICalculateReactions, CalculateReactions>();
            services.AddScoped<IRunCircularProfileStaticAnalysis, RunCircularProfileStaticAnalysis>();
            services.AddScoped<IRunRectangularProfileStaticAnalysis, RunRectangularProfileStaticAnalysis>();
            services.AddScoped<IRunHalfCarDynamicAnalysis, RunHalfCarDynamicAnalysis>();

            // Register numerical methods.
            services.AddScoped<INewmarkMethod, NewmarkMethod>();
            services.AddScoped<INewmarkBetaMethod, NewmarkBetaMethod>();

            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerDocs();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
