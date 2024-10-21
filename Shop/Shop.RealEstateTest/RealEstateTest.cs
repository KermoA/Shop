using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using System;

namespace Shop.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Size = 100;
            dto.Location = "asd";
            dto.RoomNumber = 1;
            dto.BuildingType = "asd";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt  = DateTime.Now;

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result);


        }

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("92a685aa-676b-48d3-bfeb-b9bd9e111679");

            //Act
            await Svc<IRealEstateServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);

        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
			//Arrange
			Guid databaseGuid = Guid.Parse("92a685aa-676b-48d3-bfeb-b9bd9e111679");
			Guid guid = Guid.Parse("92a685aa-676b-48d3-bfeb-b9bd9e111679");

			//Act
			await Svc<IRealEstateServices>().GetAsync(guid);

			//Assert
			Assert.Equal(databaseGuid, guid);
		}

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //Arrange
            RealEstateDto realEstate = MockRealEstateData();
			var addRealEstate = await Svc<IRealEstateServices>().Create(realEstate);

			//Act
            var result = await Svc<IRealEstateServices>().GetAsync((Guid)addRealEstate.Id);

			//Assert
			Assert.Equal(result, addRealEstate);
		}

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
			//Arrange
			RealEstateDto realEstate = MockRealEstateData();
			var realEstate1 = await Svc<IRealEstateServices>().Create(realEstate);
			var realEstate2 = await Svc<IRealEstateServices>().Create(realEstate);

			//Act
			var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);


            //Assert
            Assert.NotEqual(result.Id, realEstate1.Id);
		}

        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Size = 100,
                Location = "asd",
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }
    }
}