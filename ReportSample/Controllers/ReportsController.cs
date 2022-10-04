using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace ReportSample.Controllers
{
    [Route("api/reports")]
    public class ReportsController : ReportsControllerBase
    {
        private IWebHostEnvironment _environment;

        public ReportsController(IReportServiceConfiguration reportServiceConfiguration, IWebHostEnvironment environment)
              : base(reportServiceConfiguration)
        {
            _environment = environment;
        }
        [HttpGet("reportlist")]
        public IEnumerable<string> GetReports()
        {
            var test = Path.Combine(_environment.WebRootPath, "Reports");
            return Directory
                .GetFiles(test)
                .Select(path =>
                    Path.GetFileName(path));
        }
        protected override HttpStatusCode SendMailMessage(MailMessage mailMessage)
        {
            throw new System.NotImplementedException("This method should be implemented in order to send mail messages");
            //using (var smtpClient = new SmtpClient("smtp01.mycompany.com", 25))
            //{
            //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    smtpClient.EnableSsl = false;
            //    smtpClient.Send(mailMessage);
            //}
            //return HttpStatusCode.OK;
        }
    }
}
