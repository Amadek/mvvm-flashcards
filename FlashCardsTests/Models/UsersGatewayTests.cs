using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MySql.Data.MySqlClient;

namespace FlashCards.Models.Tests
{
    [TestClass()]
    public class UsersGatewayTests
    {
        private MySqlConnection _connection;

        public UsersGatewayTests()
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
        public void GetUserIdTest()
        {
            _connection.Open();

            // Arrange:
            var mock = new Mock<IDatabase>();
            mock.SetupGet(x => x.Connection).Returns(_connection);

            var gateway = new UsersGateway();

            // Act:
            int result = gateway.GetUserId("TestUser", mock.Object);

            // Assert:
            Assert.AreEqual(1, result);

            _connection.Close();
        }

        [TestMethod()]
        public void IsValidTest()
        {
            _connection.Open();

            // Arrange:
            var mock = new Mock<IDatabase>();
            mock.SetupGet(x => x.Connection).Returns(_connection);

            var gateway = new UsersGateway();

            // Act:
            bool result = gateway.IsValid("TestUser", "test", mock.Object);

            // Assert:
            Assert.AreEqual(true, result);

            _connection.Close();
        }

        [TestMethod()]
        public void IsInDBTest()
        {
            _connection.Open();

            // Arrange:
            var mock = new Mock<IDatabase>();
            mock.SetupGet(x => x.Connection).Returns(_connection);

            var gateway = new UsersGateway();

            // Act:
            bool result = gateway.IsInDB("TestUser", mock.Object);

            // Assert:
            Assert.AreEqual(true, result);

            _connection.Close();
        }
    }
}