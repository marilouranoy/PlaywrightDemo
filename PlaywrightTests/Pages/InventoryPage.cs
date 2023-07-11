using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages
{
    public class InventoryPage
    {
        private readonly IPage _page;
        public InventoryPage(IPage page) => _page = page;
        private ILocator IconShoppingCart => _page.Locator("#shopping_cart_container a");
        private ILocator BtnContinueShopping => _page.Locator("[data-test=\"continue-shopping\"]");
        private ILocator BtnAddToCart => _page.Locator("[data-test=\"add-to-cart-sauce-labs-backpack\"]");
        private ILocator LinkItemInCart => _page.GetByRole(AriaRole.Link, new() { Name = "Sauce Labs Backpack" });

        public ILocator GetLinkItemInCart()
        {
            return LinkItemInCart;
        }

        //click shopping cart icon
        public async Task ClickShoppingCartIcon()
        {
            await IconShoppingCart.ClickAsync();
        }

        public async Task AddItemToCart()
        {
            await BtnAddToCart.ClickAsync();
        }
    }
}
