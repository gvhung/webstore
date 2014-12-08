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
    public class CartControllerTest
    {

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //Arrange
            Mock<IBuisnessLogicLayer<Product, int>> mock = new Mock<IBuisnessLogicLayer<Product, int>>();
            mock.Setup(m => m.ReadAll()).Returns(new Product[]
            {
                new Product {Id = 1, Name = "as",},
                new Product {Id = 2, Name = "adf"},
                new Product {Id = 3, Name = "asfg"}
            }.AsQueryable());
            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);
            //Act
            controller.AddToCart(cart, 1, null);
            controller.AddToCart(cart, 2, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 2);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.Id, 1);
            Assert.AreEqual(cart.Lines.ToArray()[1].Product.Id, 2);
        }

        [TestMethod]
        public void Can_Remove_To_Cart()
        {
            //Arrange
            Mock<IBuisnessLogicLayer<Product, int>> mock = new Mock<IBuisnessLogicLayer<Product, int>>();
            mock.Setup(m => m.ReadAll()).Returns(new Product[]
            {
                new Product {Id = 1, Name = "as",},
                new Product {Id = 2, Name = "adf"},
                new Product {Id = 3, Name = "asfg"}
            }.AsQueryable());
            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);
            //Act
            controller.AddToCart(cart, 1, null);
            controller.AddToCart(cart, 2, null);

            controller.RemoveFromCart(cart, 1, null);
            controller.RemoveFromCart(cart, 2, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 0);         
        }

       //[TestMethod]
       //public void Can_Redirect_To_Cart()
       //{
       //    //Arrange
       //    Mock<IBuisnessLogicLayer<Product, int>> mock = new Mock<IBuisnessLogicLayer<Product, int>>();
       //    mock.Setup(m => m.ReadAll()).Returns(new Product[]
       //    {
       //        new Product {Id = 1, Name = "LG"},
       //        new Product {Id = 2, Name = "Sony"},
       //        new Product {Id = 3, Name = "Lenovo"}
       //    }.AsQueryable());
       //    Cart cart = new Cart();
       //    CartController controller = new CartController(mock.Object,null);
       //    //Act
       //    RedirectToRouteResult result = controller.AddToCart(cart, 1, "redirectUrl");
       //
       //    //Assert
       //    Assert.AreEqual(result.RouteValues["action"].ToString(), "Index");
       //    Assert.AreEqual(result.RouteValues["returnUrl"], "redirectUrl");
       //}

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange
            Cart cart = new Cart();
            CartController controller = new CartController(null, null);

            // Act
            CartIndexViewModel result = (CartIndexViewModel)controller.Index(cart, "Url").ViewData.Model;

            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "Url");
        }
        //[TestMethod]
        //public void Can_Login_With_Valid_Credentials()
        //{
        //    // Arrange
        //    var user = new LogOnModel();
        //    user.UserName = "7rubin";
        //    user.Password = "2223327";
        //    user.RememberMe = false;
        //
        //    var target = new AccountController();
        //
        //    // Act
        //    ActionResult result = target.Login(user, "/URL");
        //
        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectResult));
        //    Assert.AreEqual("/URL", ((RedirectResult)result).Url);
        //}
    }
}
