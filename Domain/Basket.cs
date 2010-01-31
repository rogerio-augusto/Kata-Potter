using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Basket
    {
        public Dictionary<int, int> Books
        {
            get { return GetBasketItemsFromDiscountGroups(); }
        }

        private List<List<int>> discountGroups;
        private Hashtable discountValues;
        private decimal unitPrice = 8;

        public Basket()
        {
            this.discountGroups = new List<List<int>>() { new List<int>() };
            this.loadDiscountRanges();
        }

        public void AddBook(int bookID, int qtde)
        {
            this.discountGroups.ForEach(group => group.Remove(bookID));
        
            for (int i = 0; i < qtde; i++)
            {
                if (BookExistsInAllDiscountGroups(bookID))
                    CreateNewDiscountGroupFor(bookID);
                else
                    AddBookOnAvaliableDiscountGroup(bookID);
            }
        }

        public decimal TotalValue()
        {
            var total = 0m;

            this.discountGroups.ForEach(discountGroup =>
                total += unitPrice * discountGroup.Count * (decimal)discountValues[discountGroup.Count]);

            return total;
        }

        private void AddBookOnAvaliableDiscountGroup(int bookID)
        {
            discountGroups.First(group => !group.Contains(bookID)).Add(bookID);
        }

        private void CreateNewDiscountGroupFor(int bookID)
        {
            var newDiscountGroup = new List<int>() { bookID };
            this.discountGroups.Add(newDiscountGroup);
        }

        private bool BookExistsInAllDiscountGroups(int bookID)
        {
            foreach (var group in this.discountGroups)
                if (!group.Contains(bookID)) return false;

            return true;
        }

        private void loadDiscountRanges()
        {
            this.discountValues = new Hashtable();
            discountValues.Add(1, 1m);
            discountValues.Add(2, 0.95m);
            discountValues.Add(3, 0.9m);
            discountValues.Add(4, 0.8m);
            discountValues.Add(5, 0.75m);
        }

        private Dictionary<int, int> GetBasketItemsFromDiscountGroups()
        {
            var basketItems = new Dictionary<int, int>();

            foreach (var discountGroup in discountGroups)
            {
                foreach (var book in discountGroup)
                {
                    if (basketItems.ContainsKey(book))
                        basketItems[book]++;
                    else
                        basketItems.Add(book, 1);
                }
            }

            return basketItems;
        }
    }
}