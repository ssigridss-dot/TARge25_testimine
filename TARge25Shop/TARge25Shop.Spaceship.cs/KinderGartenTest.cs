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
    }
}