using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Pages;
using PlaywrightTests.Reporting;

namespace PlaywrightTests.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTests : BaseTest
{
    [Test]
    [Category("Login")]
    public async Task LoginWithValidCredentials()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
        LoginPage loginPage = new(Page);
        await loginPage.Login("standard_user", "secret_sauce");
        await Expect(loginPage.GetIconShoppingCart()).ToBeVisibleAsync();
    }

    [Test]
    public async Task LoginWithInvalidCredentials()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
        LoginPage loginPage = new(Page);
        await loginPage.Login("standard_user", "secret_sauces");
        await Expect(loginPage.GetErrorInvalidLogin()).ToBeVisibleAsync();
        await Page.ScreenshotAsync(new()
        {
            Path = "screenshotInvalid.png",
        });
    }
}