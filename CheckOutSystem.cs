using System.Collections.Generic;
using CheckOutSystemTest.Test;

namespace CheckOutSystemTest
{
    public class CheckOutSystem
    {
        private decimal _total;

        public decimal CheckOut(List<Item> listOfItems)
        {
            foreach (var item in listOfItems) _total += item.Price;
            return _total;
        }

        public void AddItems(List<Product> listOfItems)
        {
            foreach (var item in listOfItems) Item.Add(item.Name, item.Type);
        }

        public void UpdateItem(Product existingProduct, Product updatedProduct)
        {
            Item.UpdateItem(existingProduct, updatedProduct);
        }

        public void RemoveItems(List<Product> listOfProduct)
        {
            foreach (var product in listOfProduct) Item.RemoveItem(product.Name, product.Type);
        }
    }
}