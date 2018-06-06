using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models.Tests
{
    [TestClass()]
    public class DBManagerTests
    {
        [TestMethod()]
        public void GetUserIdTest()
        {
            int expected = 1;
            int result = DBManager.GetInstance().GetUserId("Amadek");

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetCardsTest()
        {
            List<Flier> expected = new List<Flier>()
            {
                new Flier() { Polish = "ąę", English = "ae" }
            };

            List<Flier> result = DBManager.GetInstance().GetCards(1, "Kategorią 1");

            Assert.AreEqual(expected, result);
        }
    }
}