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
            /* users
             * +----+----------+----------+
             * | id | nick     | password |
             * +----+----------+----------+
             * |  1 | TestUser | test     |
             * +----+----------+----------+ 
             */
            
            int expected = 1;
            int result = DBManager.GetInstance().GetUserId("TestUser");

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetCardsTest()
        {
            /* cards
             * +----+---------+------------------+-----------------------------+
             * | id | user_id | unit             | content                     |
             * +----+---------+------------------+-----------------------------+
             * | 1  | 1       | Testowy Rozdział | pies;dog/kot;cat/dłoń;hand/ |
             * +----+---------+------------------+-----------------------------+
             */

            List<string[]> expected = new List<string[]>()
            {
                new string[2] { "pies", "dog" },
                new string[2] { "kot", "cat"},
                new string[2] { "dłoń", "hand" }
            };

            List<string[]> result = DBManager.GetInstance().GetCards(1, "Testowy Rozdział");

            for (int i = 0; i < expected.Count; i++)
            {
                // Assert polish.
                Assert.AreEqual(expected[i][0], result[i][0]);
                // Assert english.
                Assert.AreEqual(expected[i][1], result[i][1]);
            }
        }

        [TestMethod()]
        public void GetUnitsTest()
        {
            /* cards
             * +----+---------+--------------------+-----------------------------+
             * | id | user_id | unit               | content                     |
             * +----+---------+--------------------+-----------------------------+
             * | 1  | 1       | Testowy Rozdział   | pies;dog/kot;cat/dłoń;hand/ |
             * | 2  | 1       | Testowy Rozdział 2 | ąę;ae/źć;zc/                |
             * +----+---------+--------------------+-----------------------------+
             */
            
            List<string> expected = new List<string>()
            {
                "Testowy Rozdział",
                "Testowy Rozdział 2"
            };

            List<string> result = DBManager.GetInstance().GetUnits(1);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod()]
        public void IsValidTest()
        {
            bool expected = true;
            bool result = DBManager.GetInstance().IsValid("TestUser", "test");

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void IsNotValidTest()
        {
            bool expected = false;
            bool result = DBManager.GetInstance().IsValid("TestUser", "wrong_pass");

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void IsNickInDBTest()
        {
            bool expected = true;
            bool result = DBManager.GetInstance().IsNickInDB("TestUser");

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void IsNickNotInDBTest()
        {
            bool expected = false;
            bool result = DBManager.GetInstance().IsNickInDB("NotExistingUser");

            Assert.AreEqual(expected, result);
        }
    }
}