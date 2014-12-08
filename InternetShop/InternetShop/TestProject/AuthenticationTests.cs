using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Infrastructure;
using WebUI.Models.ForActivation;

namespace TestProject
{
    [TestClass]
    public class AuthenticationTests
    {
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

       // [TestMethod]
       // public void Cannot_Login_With_Invalid_Credentials()
       // {
       //     // Arrange
       //     var mock = new Mock<IAuthProvider>();
       //     mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
       //     var model = new LoginViewModel
       //     {
       //         UserName = "badUser",
       //         Password = "badPass"
       //     };
       //     var target = new AccountController(mock.Object);
       //
       //     // Act
       //     ActionResult result = target.Login(model, "/URL");
       //
       //     // Assert
       //     Assert.IsInstanceOfType(result, typeof(ViewResult));
       //     Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
       // }
    }
}
