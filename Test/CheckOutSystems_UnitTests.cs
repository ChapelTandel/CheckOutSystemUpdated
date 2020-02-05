using NUnit.Framework;
using System.Collections.Generic;

namespace CheckOutSystemTest.Test
{
    [TestFixture]
    public class WhenIHaveNothingInTheList
    {
        [Test]
        public void ThenTheTotalCostIsZero()
        {
            //Arrange
            var checkOutSystem = new CheckOutSystem();
            const decimal expectedResult = 0.00m;
            var listOfItems = new List<Item>();

            //Action
            var actualResult = checkOutSystem.CheckOut(listOfItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

    [TestFixture]
    public class WhenIHaveOnlyOneItemInTheList
    {
        //Arrange
        private static readonly object[] SourceList =
        {
            new object[] {new List<Item> {new Item("ChickenWings")}, 4.40m}, // Only one Starter in the list
            new object[] {new List<Item> {new Item("Lasagna")}, 7.00m} // Only one Main in the list
        };

        [TestCaseSource(nameof(SourceList))]
        public void ThenTheTotalCostIsTheCostOfThatItem(List<Item> listOfItems, decimal expectedResult)
        {
            var checkOutSystem = new CheckOutSystem();

            //Action
            var actualResult = checkOutSystem.CheckOut(listOfItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

    [TestFixture]
    public class WhenIHaveTwoItemsInTheList
    {
        //Arrange
        private static readonly object[] SourceList =
        {
            new object[]
            {
                new List<Item> {new Item("ChickenWings"), new Item("Samosas")}, 8.80m
            }, // 2 Starters
            new object[]
                {new List<Item> {new Item("Lasagna"), new Item("FishAndChips")}, 14.00m}, // 2 Mains
            new object[]
            {
                new List<Item> {new Item("ChickenWings"), new Item("FishAndChips")}, 11.40m
            } // 1 Starter, 1 Main
        };

        [TestCaseSource(nameof(SourceList))]
        public void ThenTheTotalCostIsTheCostOfThoseTwoItems(List<Item> listOfItems, decimal expectedResult)
        {
            var checkOutSystem = new CheckOutSystem();

            //Action
            var actualResult = checkOutSystem.CheckOut(listOfItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

    [TestFixture]
    public class WhenIHaveMoreThanTwoItemsInTheList
    {
        //Arrange
        private static readonly object[] SourceList =
        {
            new object[]
            {
                new List<Item>
                    {new Item("ChickenWings"), new Item("Samosas"), new Item("Lasagna")},
                15.80m
            }, // 2 Starters, 1Main
            new object[]
            {
                new List<Item>
                {
                    new Item("Lasagna"), new Item("FishAndChips"),
                    new Item("ChickenWings")
                },
                18.40m
            }, // 2 Mains, 1 Starter
            new object[]
            {
                new List<Item>
                {
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("FishAndChips"),
                    new Item("ChickenWings"),
                    new Item("Lasagna"),
                    new Item("Samosas"),
                    new Item("RoastChicken")
                },
                134.80m
            } // 2 Starter, 18 Main 
        };

        [TestCaseSource(nameof(SourceList))]
        public void ThenTheTotalCostIsTheCostOfAllTheItemsInTheList(List<Item> listOfItems, decimal expectedResult)
        {
            var checkOutSystem = new CheckOutSystem();

            //Action
            var actualResult = checkOutSystem.CheckOut(listOfItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

    [TestFixture]
    public class WhenIAddANewProductAndTakeItToCheckOut
    {

        [Test]
        public void ThenTheTotalCostIsTheCostOfNewItem()
        {
            //Arrange
            var listOfProduct = new List<Product> {new Product{Name = "ChickenSoup", Type = "Starter"}};
            var checkOutSystem = new CheckOutSystem();
            var expectedResult = 4.40M;

            //Action
            checkOutSystem.AddItems(listOfProduct);
            var listOfItems = new List<Item> {new Item("ChickenSoup")};
            var actualResult = checkOutSystem.CheckOut(listOfItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
            checkOutSystem.RemoveItems(listOfProduct); //Clean up 
        }
    }

    [TestFixture]
    public class WhenIUpdateAProductAndTakeItToCheckOut
    {
        //Arrange

        [Test]
        public void ThenTheTotalCostIsTheCostOfNewItem()
        {
            var existingProduct = new Product {Name = "ChickenSoup", Type = "Starter"};
            var newProduct = new Product {Name = "MiniPizza", Type = "Starter"};
            var checkOutSystem = new CheckOutSystem();
            var expectedResult = 4.40m;

            //Action
            checkOutSystem.UpdateItem(existingProduct, newProduct);
            var listOfItems = new List<Item> { new Item("MiniPizza") };
            var actualResult = checkOutSystem.CheckOut(listOfItems);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

    [TestFixture]
    public class WhenIRemoveAProduct
    {

        [Test]
        public void TheTheProductIsNoLongerAvailable()
        {
            //Arrange
            var checkOutSystem = new CheckOutSystem();
            var listOfProduct = new List<Product> { new Product { Name = "SweetCorn", Type = "Starter" } }; //Add a new product
            var listOfProductToRemove = new List<Product> { new Product { Name = "SweetCorn", Type = "Starter" } }; //Add a new product
            checkOutSystem.AddItems(listOfProduct);

            //Action
            checkOutSystem.RemoveItems(listOfProductToRemove);
            var listOfItems = new List<Item> { new Item("SweetCorn") };
            var actualResult = checkOutSystem.CheckOut(listOfItems); // Try to checkout deleted product

            //Assert
            Assert.AreEqual(0.00m, actualResult);
        }
    }


}