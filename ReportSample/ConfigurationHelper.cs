namespace ReportSample
{
    public class ConfigurationHelper
    {
        public static IConfiguration ResolveConfiguration(IWebHostEnvironment environment)
        {
            var reportingConfigFileName = System.IO.Path.Combine(environment.ContentRootPath, "reportingAppSettings.json");
            return new ConfigurationBuilder()
             .AddJsonFile(reportingConfigFileName, true)
             .Build();
        }
    }
}
