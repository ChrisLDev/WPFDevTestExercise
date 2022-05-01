using AutoFixture;
using AutoMapper;
using DataAccess.EntityFramework;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTesting;
using UserInfoFramework;
using UserInfoFramework.Data;
using UserInfoFramework.Mappings;
using UserInfoFramework.Models;
using Xunit;

namespace UserFramework.UnitTests
{
    public class UserInfoServiceTests 
    {
        private Mock<IRepository<UserInfoEntity>> _repoMock;
        private readonly IFixture _fixture;
        private readonly IMapper _mapper;

        private IUserInfoService SUT { get; }

		public UserInfoServiceTests()
		{
            _fixture = new Fixture()
                    .CustomizeWithAutoMoq()
                    .CustomizeWithAutoMapper(typeof(UserServiceMappings));

            _repoMock = _fixture
                .Freeze<Mock<IRepository<UserInfoEntity>>>();

            _mapper = _fixture.Create<IMapper>();

            SUT = _fixture
                .Create<UserInfoService>();
        }

        [Fact]
        public async Task GetAllUsers_ReturnsIEnumerableOfUsers()
        {
            // Arrange
            var expected = _fixture
                    .CreateMany<User>();

            _repoMock
                    .Setup(mock => mock.QueryAsync(
                        It.IsAny<Expression<Func<UserInfoEntity, bool>>>(),
                        It.IsAny<Expression<Func<UserInfoEntity, UserInfoEntity>>>(),
                        It.IsAny<Func<IQueryable<UserInfoEntity>, IOrderedQueryable<UserInfoEntity>>>(),
                        It.IsAny<bool>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>(),
                        It.IsAny<Func<IQueryable<UserInfoEntity>, IQueryable<UserInfoEntity>>[]>()))
                    .Returns(Task.FromResult(_mapper.Map<IEnumerable<UserInfoEntity>>(expected).ToArray()));

            // Act
            var actual = await SUT.GetAllUsers()
                .ConfigureAwait(false);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }

        [Fact]
        public async Task GetUser_ReturnsASingleUser()
        {
            // Arrange
            var expected = _fixture
                .Build<User>()
                .Create();

            _repoMock
                .Setup(mock => mock.GetAsync(
                    It.IsAny<Expression<Func<UserInfoEntity, bool>>>(),
                    It.IsAny<Func<IQueryable<UserInfoEntity>, IQueryable<UserInfoEntity>>[]>()))
                .Returns(Task.FromResult(_mapper.Map<UserInfoEntity>(expected)));

            // Act
            var actual = await SUT
                .GetUserInfo(It.IsAny<Guid>())
                .ConfigureAwait(false);

            // Assert
            expected.Should().BeEquivalentTo(actual);
        }
    }
}