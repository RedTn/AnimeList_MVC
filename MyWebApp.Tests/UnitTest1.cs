using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWebApp.Controllers;
using System.Web.Mvc;

namespace MyWebApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HomeAboutActionReturnsAboutView()
        {
            var homeController = new HomeController();
            var result = homeController.About() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

    }
}
