using System;
using System.Collections.Generic;
using System.IO;
using CheckOutSystemTest.Test;
using Newtonsoft.Json;

namespace CheckOutSystemTest
{
    public class Item
    {
        public Item(string productName)
        {
            Price = GetPrice(productName);
        }

        public decimal Price { get; set; }

        private static IDictionary<string, string> GetProductDictionary()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(
                File.ReadAllText($"{Environment.CurrentDirectory}/ProductList.json"));
        }

        public decimal GetPrice(string productName)
        {
            var productDictionary = GetProductDictionary();

            if (!productDictionary.ContainsKey(productName)) return 0.00m;
            
            var  productType = productDictionary[productName];
            return productType == "Main" ? 7.00m : 4.40m;
        }

        public static void Add(string name, string type)
        {
            var productDictionary = GetProductDictionary();
            if (productDictionary.ContainsKey(name)) return;
            productDictionary.Add(name, type);
            WriteDictionaryToTheJsonFile(productDictionary);
        }

        private static void WriteDictionaryToTheJsonFile(IDictionary<string, string> productDictionary)
        {
            File.WriteAllText(
                $"{Environment.CurrentDirectory}/ProductList.json",
                JsonConvert.SerializeObject(productDictionary));
        }

        public static void UpdateItem(Product existingProduct, Product updatedProduct)
        {
            var productDictionary = GetProductDictionary();
            if(productDictionary.ContainsKey(existingProduct.Name))
                productDictionary.Remove(existingProduct.Name);

            if(!productDictionary.ContainsKey(updatedProduct.Name))
                productDictionary.Add(updatedProduct.Name, updatedProduct.Type);
            
            WriteDictionaryToTheJsonFile(productDictionary);
        }

        public static void RemoveItem(string name, string type)
        {
            var productDictionary = GetProductDictionary();
            if (productDictionary.ContainsKey(name)) productDictionary.Remove(name);
            WriteDictionaryToTheJsonFile(productDictionary);
        }
    }
}