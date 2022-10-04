using AppCore.ReportModels;
using Telerik.Reporting;
using Telerik.Reporting.Services;
namespace ReportSample
{
    public class CustomReportResolver : IReportSourceResolver
    {
        public ReportSource Resolve(string report, OperationOrigin operationOrigin, IDictionary<string, object> currentParameterValues)
        {



            var reportPackager = new ReportPackager();
            Report reportt = null;
            using (var sourceStream = System.IO.File.OpenRead($"wwwroot\\Reports\\{report}"))
            {
                reportt = (Report)reportPackager.UnpackageDocument(sourceStream);
            }

            DetailSection detail = (DetailSection)reportt.Items["detailSection1"];
            Table table = (Table)detail.Items["table2"];
            
            table.DataSource = currentParameterValues["Parameter1"];
          
            InstanceReportSource instanceReportSource = new InstanceReportSource();
            instanceReportSource.ReportDocument = reportt;            
            return instanceReportSource;




        }
    }
}
