using System;
using System.Linq;
using System.Runtime;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using BuisnessLogicLayer;
using Entities;
using WebUI.Controllers;
using WebUI.Models;
using Moq;
using WebUI.Models.ForActivation;

namespace TestProject
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Can_Edit_Product()
        {
            Mock<IBuisnessLogicLayer<Product, int>> mock = new Mock<IBuisnessLogicLayer<Product, int>>();
            mock.Setup(m => m.ReadAll()).Returns(new Product[] 
            {
                new Product {Id = 1, Name = "asd"},
                new Product {Id = 2, Name = "asdfadf"},
                new Product {Id = 3, Name = "asdf"},
              }.AsQueryable());
            var target = new AdminController(mock.Object, null);

            // Act
            var p1 = target.Edit(1).ViewData.Model as Product;
            var p2 = target.Edit(2).ViewData.Model as Product;
            var p3 = target.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, p1.Id);
            Assert.AreEqual(2, p2.Id);
            Assert.AreEqual(3, p3.Id);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange
            Mock<IBuisnessLogicLayer<Product, int>> mock = new Mock<IBuisnessLogicLayer<Product, int>>();
            mock.Setup(m => m.ReadAll()).Returns(new Product[] 
            {
                new Product {Id = 1, Name = "asdf"},
                new Product {Id = 2, Name = "asdfaf"},
                new Product {Id = 3, Name = "asdfasf"},
              }.AsQueryable());
            var target = new AdminController(mock.Object, null);
        
            // Act
            var result = (Product)target.Edit(4).ViewData.Model;
        
            // Assert
            Assert.IsNull(result);
        }
        
        
        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            // Arrange
            Mock<IBuisnessLogicLayer<Product, int>> mock = new Mock<IBuisnessLogicLayer<Product, int>>();
            mock.Setup(m => m.ReadAll()).Returns(new Product[]  
            {
                new Product {Id = 1, Name = "asdf"},
                new Product {Id = 2, Name = "asdff"},
                new Product {Id = 3, Name = "asdfsaf"},
              }.AsQueryable());
            var target = new AdminController(mock.Object, null);
            const int id = 2;
        
            // Act 
            target.Delete(id);
        
            // Assert
            mock.Verify(m => m.Delete(id));
        }
    }
}
