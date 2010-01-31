using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PotterDomainTest
{
    [TestClass()]
    public class BasketTest
    {
        Basket basket;

        [TestInitialize]
        public void BeforeEach()
        {
            this.basket = new Basket();
        }

        [TestMethod()]
        public void ShouldAddNewItemsToTheBasket()
        {
            var itemID = 1;
            var qtde = 1;

            this.basket.AddBook(itemID, qtde);

            Assert.IsTrue(this.basket.Books.ContainsKey(itemID) && this.basket.Books[itemID] == qtde, "Basket does not contain the required item");
        }

        [TestMethod()]
        public void BasketShouldReplaceTheQuantityForExistingItems()
        {
            var itemID = 1;
            var qtde = 1;
            var newQtde = 4;

            this.basket.AddBook(itemID, qtde);
            this.basket.AddBook(itemID, newQtde);

            Assert.IsTrue(this.basket.Books.ContainsKey(itemID) && basket.Books[itemID] == newQtde, "Error changing the item quantity");
        }

        [TestMethod()]
        public void BasketShouldNotAddItemsWithZeroQuantity()
        {
            var itemID = 1;

            this.basket.AddBook(itemID, 0);

            Assert.IsFalse(this.basket.Books.ContainsKey(itemID), "Basket should not accept zero quantity");
        }

        [TestMethod()]
        public void BasketShouldRemoveItemsWithZeroQuantity()
        {
            var itemID = 1;

            this.basket.AddBook(itemID, 5);
            Assert.IsTrue(this.basket.Books.ContainsKey(itemID), "Item was not inserted");

            this.basket.AddBook(itemID, 0);

            Assert.IsFalse(this.basket.Books.ContainsKey(itemID), "Quantity changed to zero should remove the item from the basket");
        }
    }
}
