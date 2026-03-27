using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.pages;

namespace UnitTestAuth
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        public void AuthTest()
        {
            var page = new LoginPage();
            bool result = page.Auth("неверный", "логин");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AuthTestSuccess()
        {
            var page = new LoginPage();
            Assert.IsTrue(page.Auth("Elizor@gmai.com", "yntiRS"), "Не удалось авторизоваться как администратор");
            Assert.IsTrue(page.Auth("Vladlena@gmai.com", "07i7Lb"), "Не удалось авторизоваться как Менеджер A");
            Assert.IsTrue(page.Auth("Adam@gmai.com", "7SP9CV"), "Не удалось авторизоваться как Менеджер C");
        }

        [TestMethod]
        public void AuthTestFail()
        {
            var page = new LoginPage();
            Assert.IsFalse(page.Auth("", ""), "Пустые поля должны возвращать false");
            Assert.IsFalse(page.Auth("Elizor@gmai.com", "yntirs"), "Неверный пароль");
            Assert.IsFalse(page.Auth("Elizor@gmail.com", "yntiRS"), "Неверный логин");
        }
    }
}