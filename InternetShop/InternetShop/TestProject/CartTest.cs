using System;
using System.Linq;
using System.Runtime;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BuisnessLogicLayer;
using Entities;
using WebUI.Controllers;
using WebUI.Models;
using Moq;

namespace TestProject
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - создание условий для теста
            Product p1 = new Product { Id = 1, Name = "asd" };
            Product p2 = new Product { Id = 2, Name = "adsff" };
            Product p3 = new Product { Id = 3, Name = "Leasdfasnovo" };

            Cart cart = new Cart();

            // Act - выполнение теста
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 3);

            CartLine[] results = cart.Lines.ToArray();

            // Assert - проверка резултатов
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
            Assert.AreEqual(results[2].Product, p3);
        }

        [TestMethod]
        public void Can_Add_Quantity()
        {
            // Arrange - создание условий для теста
            Product p1 = new Product { Id = 1, Name = "as" };
            Product p2 = new Product { Id = 2, Name = "Soasdfny" };
            Product p3 = new Product { Id = 3, Name = "asdf" };

            Cart cart = new Cart();

            // Act - выполнение теста
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 3);
            cart.AddItem(p1, 3);
            cart.AddItem(p2, 1);
            CartLine[] results = cart.Lines.ToArray();

            // Assert - проверка резултатов
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0].Quantity, 4);
            Assert.AreEqual(results[1].Quantity, 3);
            Assert.AreEqual(results[2].Quantity, 3);
        }

        [TestMethod]
        public void Can_Remove_Lines()
        {
            // Arrange - создание условий для теста
            Product p1 = new Product { Id = 1, Name = "as" };
            Product p2 = new Product { Id = 2, Name = "asdf" };
            Product p3 = new Product { Id = 3, Name = "asdff" };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 3);
            cart.AddItem(p1, 3);
            cart.AddItem(p2, 1);

            // Act - выполнение теста
            cart.RemoveLine(p1);

            // Assert - проверка резултатов
            Assert.AreEqual(cart.Lines.Count(), 2);
            Assert.AreEqual(cart.Lines.Where(p => p.Product == p1).Count(), 0);
        }

        [TestMethod]
        public void Calculate_Total_Price()
        {
            // Arrange - создание условий для теста
            Product p1 = new Product { Id = 1, Name = "as", Price = 300 };
            Product p2 = new Product { Id = 2, Name = "Sasdony", Price = 600 };
            Product p3 = new Product { Id = 3, Name = "asdf", Price = 200 };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 3);

            // Act - выполнение теста
            decimal totalPrice = cart.ComputeTotalValue();

            // Assert - проверка резултатов
            Assert.AreEqual(totalPrice, 2100);
        }
    }
}
