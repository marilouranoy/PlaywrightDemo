using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests.Pages;

public class LoginPage
{
    private readonly IPage _page;
    public LoginPage(IPage page) => _page = page;

    private ILocator TextUserName => _page.Locator("[data-test=\"username\"]");
    private ILocator TextPassword => _page.Locator("[data-test=\"password\"]");
    private ILocator BtnLogin => _page.Locator("[data-test=\"login-button\"]");
    private ILocator IconShoppingCart => _page.Locator("#shopping_cart_container a");

    private ILocator ErrorInvalidLogin => _page.GetByText("Epic sadface: Username and password do not match any user in this service");

    public ILocator GetIconShoppingCart()
    {
        return IconShoppingCart;
    }

    public ILocator GetErrorInvalidLogin()
    {
        return ErrorInvalidLogin;
    }

    public async Task Login(string username, string password)
    {
        await TextUserName.FillAsync(username);
        await
        TextPassword.FillAsync(password);
        await BtnLogin.ClickAsync();
    }
}