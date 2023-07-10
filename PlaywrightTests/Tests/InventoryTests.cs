using PlaywrightTests.Pages;
using PlaywrightTests.Reporting;

namespace PlaywrightTestsDemo.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class InventoryTests : BaseTest
    {
        [Test]
        [Category("Inventory")]
        public async Task ItemShouldBeAddedToCart()
        {
            await Page.GotoAsync("https://www.saucedemo.com/");
            LoginPage loginPage = new(Page);
            await loginPage.Login("standard_user", "secret_sauce");
            InventoryPage inventoryPage = new(Page);
            await inventoryPage.AddItemToCart();
            await inventoryPage.ClickShoppingCartIcon();
            await Expect(inventoryPage.GetLinkItemInCart()).ToBeVisibleAsync();
        }
    }
}
