using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using System;
using System.Dynamic;

namespace Shop.KindergartenTest
{
	public class KindergartenTest : TestBase
	{

		[Fact]
		public async Task Should_CreateKindergarten_WhenEmptyGroupName()
		{
			//Assert
			KindergartenDto dto = new();

			dto.Id = Guid.NewGuid();
			dto.KindergartenName = "TestKindergarten";
			dto.GroupName = string.Empty;
			dto.ChildrenCount = 10;
			dto.Teacher = "TestTeacher";

			//Act
			var result = await Svc<IKindergartensServices>().Create(dto);

			//Assert
			Assert.NotNull(result);
			Assert.Empty(result.GroupName);
		}

		[Fact]
		public async Task Should_CreateKindergarten_WhenMaxChildrenCount()
		{
			//Assert
			KindergartenDto dto = new();

			dto.Id = Guid.NewGuid();
			dto.KindergartenName = "TestKindergarten";
			dto.GroupName = "TestGroupName";
			dto.ChildrenCount = int.MaxValue;
			dto.Teacher = "TestTeacher";
			dto.CreatedAt = DateTime.Now;
			dto.UpdatedAt = DateTime.Now;

			//Act
			var result = await Svc<IKindergartensServices>().Create(dto);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(int.MaxValue, result.ChildrenCount);

		}

		[Fact]
		public async Task Should_ReturnNull_WhenDetailAsyncNonExistingKindergarten()
		{
			//Arrange
			var nonExistingId = Guid.NewGuid();

			//Act
			var result = await Svc<IKindergartensServices>().DetailAsync(nonExistingId);

			//Assert
			Assert.Null(result);
		}

		[Fact]
		public async Task ShouldNot_ChangeCreatedAt_WhenUpdateKindergarten()
		{
			// Arrange
			KindergartenDto dto = MockKindergartenData();
			var createKindergarten = await Svc<IKindergartensServices>().Create(dto);

			//Act
			dto.ChildrenCount = 100;
			var result = await Svc<IKindergartensServices>().Update(dto);

			//Assert
			Assert.Equal(result.CreatedAt, dto.CreatedAt);
			Assert.NotEqual(result.ChildrenCount, createKindergarten.ChildrenCount);
			Assert.NotEqual(result.UpdatedAt, dto.UpdatedAt);
		}

		[Fact]
		public async Task Should_DeleteExistingKindergarten_Successfully()
		{
			//Arrange
			KindergartenDto dto = MockKindergartenData();
			var kindergarten = await Svc<IKindergartensServices>().Create(dto);

			var createdKindergarten = await Svc<IKindergartensServices>().DetailAsync((Guid)kindergarten.Id);

			Assert.NotNull(createdKindergarten);

			var result = await Svc<IKindergartensServices>().Delete((Guid)kindergarten.Id);
			var deletedKindergarten = await Svc<IKindergartensServices>().DetailAsync((Guid)kindergarten.Id);

			Assert.Null(deletedKindergarten);
			Assert.NotNull(result);
		}


		private KindergartenDto MockKindergartenData()
		{
			KindergartenDto kindergarten = new()
			{
				KindergartenName = "TestKindergarten",
				GroupName = "TestGroupName",
				ChildrenCount = 10,
				Teacher = "TestTeacher",
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
			};

			return kindergarten;
		}
	}
}