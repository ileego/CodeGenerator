using System;
using System.Collections.Generic;
using System.Linq;
using CodeGenerator.Infra.Common.ForTest.Entities;
using CodeGenerator.Infra.Common.ForTest.Repository;
using CodeGenerator.Infra.Common.Implements;
using CodeGenerator.Infra.Common.Interfaces;
using CodeGenerator.Infra.Common.Utils;
using Microsoft.EntityFrameworkCore;
using UnitTest.Fixtures;
using Xunit;

namespace UnitTest
{
    public class EfTest : IClassFixture<CommonFixture>
    {
        private readonly CommonFixture _fixture;

        public EfTest(CommonFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public void Test1()
        {
            var unitOfWork = _fixture.ServiceProvider.GetService(typeof(IUnitOfWork));
            Assert.NotNull(unitOfWork);
            var tableRepository = (ITableRepository)_fixture.ServiceProvider.GetService(typeof(ITableRepository));
            var tables = tableRepository.Query.ToList();
            Assert.True(tables.Any());
            var userRepository = (IUserRepository)_fixture.ServiceProvider.GetService(typeof(IUserRepository));
            var user = userRepository.Query.Where(t => t.Id == 1).Include(t => t.UserContacts).FirstOrDefault();
            var userContact = user.UserContacts.FirstOrDefault();
            Assert.NotNull(userContact);
            var u = new User()
            {
                Id = SnowflakeId.NextId(1, 1),
                CreationTime = DateTime.Now,
                UserName = "abc",
                Password = "123"
            };
            userRepository.Insert(u);
        }
    }
}
