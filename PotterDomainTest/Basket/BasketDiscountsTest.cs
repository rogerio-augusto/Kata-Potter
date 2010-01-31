using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PotterDomainTest
{
    [TestClass()]
    public class BasketDiscountsTest
    {
        Basket basket;
        decimal singlePrice = 8m;

        [TestInitialize]
        public void BeforeEach()
        {
            this.basket = new Basket();
        }

        [TestMethod()]
        public void OneOrMoreFromTheSameItemShouldNotGetDiscount()
        {
            this.basket.AddBook(1, 1);
            Assert.AreEqual(singlePrice, this.basket.TotalValue());

            this.basket.AddBook(1, 2);
            Assert.AreEqual(singlePrice * 2, this.basket.TotalValue());
        }

        [TestMethod()]
        public void TwoDifferentBooksShouldGiveFivePercentDiscount()
        {
            this.basket.AddBook(1, 1);
            this.basket.AddBook(2, 1);
            Assert.AreEqual(singlePrice * 2 * 0.95m, this.basket.TotalValue());
        }

        [TestMethod()]
        public void ThreeDifferentBooksShouldGiveTenPercentDiscount()
        {
            this.basket.AddBook(1, 1);
            this.basket.AddBook(2, 1);
            this.basket.AddBook(3, 1);
            Assert.AreEqual(singlePrice * 3 * 0.9m, this.basket.TotalValue());
        }

        [TestMethod()]
        public void FourDifferentBooksShouldGiveTwentyPercentDiscount()
        {
            this.basket.AddBook(1, 1);
            this.basket.AddBook(2, 1);
            this.basket.AddBook(3, 1);
            this.basket.AddBook(4, 1);
            Assert.AreEqual(singlePrice * 4 * 0.8m, this.basket.TotalValue());
        }

        [TestMethod()]
        public void FiveDifferentBooksShouldGiveTwentyFivePercentDiscount()
        {
            this.basket.AddBook(1, 1);
            this.basket.AddBook(2, 1);
            this.basket.AddBook(3, 1);
            this.basket.AddBook(4, 1);
            this.basket.AddBook(5, 1);
            Assert.AreEqual(singlePrice * 5 * 0.75m, this.basket.TotalValue());
        }

        [TestMethod]
        public void BasketShouldGiveDiscountsGroupingBookSequences()
        {
            //[0, 0, 1]
            this.basket.AddBook(1, 2);
            this.basket.AddBook(2, 1);
            Assert.AreEqual(singlePrice + (singlePrice * 2 * 0.95m), this.basket.TotalValue());

            //[0, 0, 1, 1]
            this.basket.AddBook(2, 2);
            Assert.AreEqual(2 * (singlePrice * 2 * 0.95m), this.basket.TotalValue());

            //[1, 1, 2, 3, 3, 4]
            this.basket.AddBook(2, 1);
            this.basket.AddBook(3, 2);
            this.basket.AddBook(4, 1);
            Assert.AreEqual((singlePrice * 4 * 0.8m) + (singlePrice * 2 * 0.95m), this.basket.TotalValue());
            
        }
    }
}
