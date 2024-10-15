using Shop.Core.Dto;
using Shop.Core.ServiceInterface;

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
    }
}