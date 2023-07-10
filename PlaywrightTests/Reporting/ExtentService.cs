using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace PlaywrightTests.Reporting
{
    public class ExtentService
    {
        public static ExtentReports? extent;

        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                extent = new ExtentReports();
                string reportDir = Path.Combine(GetProjectRootDirectory(), "Report");
                if (!Directory.Exists(reportDir))
                    Directory.CreateDirectory(reportDir);

                string path = Path.Combine(reportDir, "index.html");
                var reporter = new ExtentHtmlReporter(path);
                reporter.Config.DocumentTitle = "Playwright Tests Report";
                reporter.Config.ReportName = "Playwright Tests Report";
                reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
                extent.AttachReporter(reporter);

                OperatingSystem os = Environment.OSVersion;
            }

            return extent;
        }

        public static string GetProjectRootDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory.Split("bin")[0];
        }
    }
}
