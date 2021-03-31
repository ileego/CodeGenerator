using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGenerator.Core.Db.Repository;
using CodeGenerator.Core.ForTest.Entities;
using CodeGenerator.Core.ForTest.Repository;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Core.Utils;
using CodeGenerator.Infra.Common.BaseEntities;
using CodeGenerator.Infra.Common.Interfaces;
using CodeGenerator.Infra.Common.Utils;
using Microsoft.EntityFrameworkCore;
using MySql.Data.Types;
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
        public void TestReflect()
        {
            var propertyInfos = typeof(Entity).GetProperties();
            Assert.NotEmpty(propertyInfos);
        }

        [Fact]
        public async Task TestRepository()
        {
            var unitOfWork = _fixture.GetService<IUnitOfWork>();
            Assert.NotNull(unitOfWork);
            var tableRepository = _fixture.GetService<ITableRepository>();
            var tables = tableRepository.Query.ToList();
            Assert.True(tables.Any());
            var columnRepository = _fixture.GetService<IColumnRepository>();
            var columns = columnRepository.Query.ToList();
            Assert.True(columns.Any());
            var tableFactory = _fixture.GetService<ITableFactory<CodeGenerator.Core.Db.Entities.Table,
                ICollection<CodeGenerator.Core.Db.Entities.Column>>>();
            var generateContext = await tableFactory.CreateContext();
            Assert.NotEmpty(generateContext.Tables);
            var firstTable = generateContext.Tables.First();
            var baseClass = firstTable.FindBaseClass();
            Assert.Equal("FullAuditEntity", baseClass);
            var fields = firstTable.Fields.ExcludeFieldsByPropertyNames(Exclude.DefaultExcludeFields());
            Assert.True(!fields.Any(t => t.PropertyName.Equals("id", StringComparison.OrdinalIgnoreCase)));
            //var userRepository = _fixture.GetService<IUserRepository>();
            //var user = userRepository.Query.Where(t => t.UserName.Equals("tao_abc_2")).Include(t => t.UserContacts).FirstOrDefault();
            //var user2 = userRepository.Query.Where(t => t.UserName.Equals("tao_abc_7")).Include(t => t.UserContacts).FirstOrDefault();
            ////Assert.NotNull(userContact);
            //var userId = SnowflakeId.NextId(1, 1);
            //var u = new User()
            //{
            //    Id = userId,
            //    CreationTime = DateTime.Now,
            //    UserName = "tao_abc_2",
            //    Password = "12332",
            //    UserContacts = new List<UserContact>()
            //    {
            //        new UserContact()
            //        {
            //            Id = SnowflakeId.NextId(1, 1),
            //            UserId = userId,
            //            ContactAddress = "231111231",
            //            ContactTelephone = "234234",
            //            Geometry = new MySqlGeometry(55.120450,42.0125420)
            //        }
            //    }
            //};
            //unitOfWork.BeginTransaction();
            //userRepository.Insert(u);
            //userRepository.Delete(user);
            //user2.UserName = "tao_abc_7";
            //user2.UserContacts.FirstOrDefault().ContactAddress = "ʲô�ط�";
            //userRepository.Update(user2);
            //var t = unitOfWork.Commit();
        }
    }
}
