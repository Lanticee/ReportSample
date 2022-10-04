using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ReportSample.Data;
using Telerik.Reporting.Services;

namespace ReportSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            builder.Services.AddScoped<IReportSourceResolver, CustomReportResolver>();
            builder.Services.TryAddSingleton<IReportServiceConfiguration>(sp =>
                        new ReportServiceConfiguration
                        {
                            ReportingEngineConfiguration = ConfigurationHelper.ResolveConfiguration(sp.GetService<IWebHostEnvironment>()),
                            HostAppId = "AppCore.Web",
                            Storage = new Telerik.Reporting.Cache.File.FileStorage(),
                            ReportSourceResolver = new CustomReportResolver(),
                            //ReportSourceResolver = new UriReportSourceResolver(
                            //    System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().WebRootPath, "reports"))
                        });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();
            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}