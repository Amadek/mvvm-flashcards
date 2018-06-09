using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Moq;

namespace FlashCards.Models.Tests
{
    [TestClass()]
    public class CardsGatewayTests
    {
        private MySqlConnection _connection;

        public CardsGatewayTests()
        {
            _connection = new MySqlConnection(
                @"server=localhost;
                database=cards;
                uid=root;password=;
                sslmode=none;
                charset=utf8"
            );
        }

        [TestMethod()]
        public void GetCardsTest()
        {
            _connection.Open();

            // Arrange:
            var mock = new Mock<IDatabase>();
            mock.SetupGet(x => x.Connection).Returns(_connection);

            var gateway = new CardsGateway();

            var expected = new List<string[]>()
            {
                new string[2] { "pies", "dog" },
                new string[2] { "kot", "cat"},
                new string[2] { "dłoń", "hand" }
            };

            // Act:
            var result = gateway.GetCards(1, "Testowy Rozdział", mock.Object);

            // Assert:
            for (int i = 0; i < expected.Count; i++)
            {
                // Assert polish:
                Assert.AreEqual(expected[i][0], result[i][0]);
                // Assert english:
                Assert.AreEqual(expected[i][1], result[i][1]);
            }

            _connection.Close();
        }

        [TestMethod()]
        public void GetUnitsTest()
        {
            _connection.Open();

            // Arrange:
            var mock = new Mock<IDatabase>();
            mock.SetupGet(x => x.Connection).Returns(_connection);

            var gateway = new CardsGateway();

            var expected = new List<string>()
            {
                "Testowy Rozdział",
                "Testowy Rozdział 2"
            };

            // Act:
            var result = gateway.GetUnits(1, mock.Object);

            // Assert:
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }

            _connection.Close();
        }
    }
}