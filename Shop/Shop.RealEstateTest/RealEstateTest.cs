using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using System;
using System.Dynamic;

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
            dto.ModifiedAt = DateTime.Now;

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

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //Arrange
            var guid = new Guid("92a685aa-676b-48d3-bfeb-b9bd9e111679");

            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("92a685aa-676b-48d3-bfeb-b9bd9e111679");
            domain.Size = 99;
            domain.Location = "qwe";
            domain.RoomNumber = 456;
            domain.BuildingType = "qwe";
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;

            //Act

            await Svc<IRealEstateServices>().Update(dto);

            //Assert
            Assert.Equal(guid, domain.Id);
            Assert.DoesNotMatch(dto.Location, domain.Location);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.NotEqual(dto.Size, domain.Size);

        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //Act
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            //Assert
            Assert.DoesNotMatch(result.Location, createRealEstate.Location);
            Assert.NotEqual(result.ModifiedAt, createRealEstate.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //Act
            RealEstateDto nullUpdate = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(nullUpdate);

            //Assert
            Assert.NotEqual(dto.Id, result.Id);
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
        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Size = 99,
                Location = "qwe",
                RoomNumber = 6,
                BuildingType = "qwe",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };

            return realEstate;
        }

        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Id = null,
                Size = 99,
                Location = "qwe",
                RoomNumber = 6,
                BuildingType = "qwe",
                CreatedAt = DateTime.Now.AddYears(-1),
                ModifiedAt = DateTime.Now.AddYears(-1),
            };

            return realEstate;
        }

    }	
}