using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGenerator.Core.Db.Repository.Column;
using CodeGenerator.Core.Db.Repository.Table;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Core.Utils;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Infra.Common.Extensions.String;
using CodeGenerator.Infra.Common.Uow;
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
            var s = "ApplicationId".RemovePostFix("Id");
            var propertyInfos = typeof(BaseEntity).GetProperties();
            Assert.NotEmpty(propertyInfos);
            var strArray = new string[2];
            strArray = "string.bac".Split(".");
            var ss = string.Join(",", strArray);
            Assert.Equal("string,bac", ss);
            object sao = strArray;
            string sa = ((string[])sao)[0];
            Assert.Equal("string", sa);

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
