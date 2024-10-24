using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using System;
using System.Dynamic;

namespace Shop.KindergartenTest
{
	public class KindergartenTest : TestBase
	{
		[Fact]
		public async Task ShouldNot_AddEmptyKindergarten_WhenReturnResult()
		{
			//Arrange
			KindergartenDto dto = new();

			dto.GroupName = "TestGroupName";
			dto.ChildrenCount = 10;
			dto.KindergartenName = "TestKindergartenName";
			dto.Teacher = "TestTeacher";
			dto.CreatedAt = DateTime.Now;
			dto.UpdatedAt = DateTime.Now;

			//Act
			var result = await Svc<IKindergartensServices>().Create(dto);

			//Assert
			Assert.NotNull(result);
		}

		[Fact]
		public async Task ShouldNot_ChangeCreatedAt_WhenUpdateKindergarten()
		{
			//Arrange
			KindergartenDto dto = new();

			dto.GroupName = "TestGroupName";
			dto.ChildrenCount = 10;
			dto.KindergartenName = "TestKindergartenName";
			dto.Teacher = "TestTeacher";
			dto.CreatedAt = DateTime.Now;
			dto.UpdatedAt = DateTime.Now;

			KindergartenDto domain = new();

			domain.GroupName = "TestGroupName2";
			domain.ChildrenCount = 10;
			domain.KindergartenName = "TestKindergartenName";
			domain.Teacher = "TestTeacher";
			domain.CreatedAt = DateTime.Now;
			domain.UpdatedAt = DateTime.Now;

			//Act
			await Svc<IKindergartensServices>().Update(dto);

			//Assert
			Assert.NotEqual(dto.CreatedAt, domain.CreatedAt);
		}

		
	}
}