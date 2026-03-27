using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.pages;

namespace UnitTestReg
{
    [TestClass]
    public class RegTests
    {
        [TestMethod]
        public void RegTestSuccess()
        {
            var page = new RegisterPage();
            bool result = page.Register("newuser_test456", "Pass123Test", "newtest456@mail.ru", "Новый Тестовый Пользователь");
            Assert.IsTrue(result, "Регистрация нового пользователя должна пройти успешно");
        }

        [TestMethod]
        public void RegTestFail()
        {
            var page = new RegisterPage();
            Assert.IsFalse(page.Register("", "", "", ""), "Пустые поля должны возвращать false");
            Assert.IsFalse(page.Register("Elizor@gmai.com", "123", "mail@mail.ru", "Имя"), "Дубликат логина должен быть отклонён");
        }
    }
}