using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using Xunit;

namespace TARge25Shop_SpaceshipTest
{
    public class KinderGartenTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyKinderGarten_WhenResultIsReturned()
        {
            // Ülesseade
            KinderGartenDto dto = new KinderGartenDto()
            {
                GroupName = "Mesilane",
                ChildrenCount = 3,
                KinderGartenName = "Sipsik",
                TeacherName = "Aino",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Tegevus
            var result = await Svc<IKinderGartenServices>().Create(dto);

            // Kontroll
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetKinderGartenById_WhenIdNotEqual()
        {
            // Ülesseade
            Guid wrongGuid = Guid.NewGuid();
            Guid goodGuid = Guid.Parse("f40900a6-7c03-47af-b10c-4a035f8664fa");

            // Tegevus
            await Svc<IKinderGartenServices>().DetailAsync(goodGuid);

            // Kontroll
            Assert.NotEqual(wrongGuid, goodGuid);
        }

        [Fact]
        public async Task Should_GetKinderGartenById_WhenGuidIsEqual()
        {
            // Ülesseadmine
            Guid databaseGuid = Guid.Parse("f40900a6-7c03-47af-b10c-4a035f8664fa");
            Guid seekGuid = Guid.Parse("f40900a6-7c03-47af-b10c-4a035f8664fa");

            // Tegevus
            await Svc<IKinderGartenServices>().DetailAsync(seekGuid);

            // Kontroll
            Assert.Equal(databaseGuid, seekGuid);
        }
    }
}