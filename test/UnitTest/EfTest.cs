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
        public void TestRepository()
        {
            var unitOfWork = _fixture.GetService<IUnitOfWork>();
            Assert.NotNull(unitOfWork);
            var tableRepository = _fixture.GetService<ITableRepository>();
            var tables = tableRepository.Query.ToList();
            Assert.True(tables.Any());
            var userRepository = _fixture.GetService<IUserRepository>();
            var user = userRepository.Query.Where(t => t.UserName.Equals("tao_abc_3")).Include(t => t.UserContacts).FirstOrDefault();
            var user2 = userRepository.Query.Where(t => t.UserName.Equals("tao_abc")).Include(t => t.UserContacts).FirstOrDefault();
            //Assert.NotNull(userContact);
            var userId = SnowflakeId.NextId(1, 1);
            var u = new User()
            {
                Id = userId,
                CreationTime = DateTime.Now,
                UserName = "tao_abc_3",
                Password = "12332",
                UserContacts = new List<UserContact>()
                {
                    new UserContact()
                    {
                        UserId = userId,
                        ContactAddress = "falfsfs",
                        ContactTelephone = "234234"
                    }
                }
            };
            unitOfWork.BeginTransaction();
            userRepository.Insert(u);
            userRepository.Delete(user);
            user2.UserName = "≤‚ ‘22";
            user2.UserContacts.FirstOrDefault().ContactAddress = " ≤√¥µÿ∑Ω";
            userRepository.Update(user2);
            var t = unitOfWork.Commit();
        }
    }
}
