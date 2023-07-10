using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests.Reporting
{
    public class BaseTest : PageTest
    {
        public string? environmentName;
        public ExtentTest? extentTest;
        public ExtentReports? extentReports;
        public string? screenShotString;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [SetUp]
        public void StartBrowser()
        {
            extentReports = ExtentService.extent;
            extentTest = ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [OneTimeTearDown]
        public async Task CloseBrowser()
        {
            //Playwright
            ExtentService.GetExtent().Flush();
            await Page.CloseAsync();
            await Context.CloseAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var errorMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);

                var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);

                switch (status)
                {
                    case TestStatus.Failed:
                        ReportLog.Fail("Test Failed");
                        ReportLog.Fail(errorMessage);
                        ReportLog.Fail(stackTrace);
                        if (extentTest != null)
                        {
                            await TakeScreenCaptureByte();
                            ReportLog.Fail(extentTest, "Screenshot", CaptureScreenshotString(TestContext.CurrentContext.Test.Name));
                        }
                        screenShotString = "";
                        break;

                    case TestStatus.Skipped:
                        ReportLog.Skip("Test Skipped");
                        break;

                    case TestStatus.Passed:
                        ReportLog.Pass("Test Passed");
                        await TakeScreenCaptureByte();
                        if (extentTest != null)
                            ReportLog.Pass(extentTest, "Screenshot", CaptureScreenshotString(TestContext.CurrentContext.Test.Name));
                        screenShotString = "";
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Exception: " + e);
            }
            finally
            {
            }
        }

        public MediaEntityModelProvider CaptureScreenshot(string screenShotPath, string name)
        {
            return MediaEntityBuilder.CreateScreenCaptureFromPath(screenShotPath, name).Build();
        }

        public MediaEntityModelProvider CaptureScreenshotString(string name)
        {
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShotString, name).Build();
        }

        public async Task TakeScreenCaptureByte()
        {
            screenShotString = Convert.ToBase64String(await Page.ScreenshotAsync());
        }
    }
}
