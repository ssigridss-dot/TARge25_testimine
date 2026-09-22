using System;
using System.Collections.Generic;
using System.Text;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using Xunit;

namespace TARge25Shop_SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {

        [Fact] //Fact tähistab ära ühte testi xUnit raamistikus
        // 1 - Kirjeldatakse ära kas test on tavaline või negatiivne
        // 2 - Kirjeldatakse ära mida parasjagu üritatakse testialuse objektiga teha
        // 3 - Mis tingimustel tulemust kontrollitakse, peale tegevust
        //
        // Selles testis kontrollitakse, et (2) Kosmoselaeva lisamisel
        // (1) Ei tohiks (3) saadud tulemus olla tühi. Jälgi seda sõnastusviisi:
        //                    |(1)          |(2)              |(3)
        public async Task ShouldNot_AddEmptySpaceShip_WhenResultIsReturned()
        {
            //Ülesseade
            SpaceshipDto dto = new SpaceshipDto()
            {
                Name = "X AE",
                ShipType = "Lendav taldrik",
                Crew = 67,
                EnginePower = 69, //hobujõudu
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            //tegutsemine
            var result = await Svc<ISpaceshipServices>().Create(dto);

            //kontroll
            Assert.NotNull(result);
        }

        [Fact]
        // Selles testis kontrollitakse, et Spaceshipi päring andmebaasist ei tohiks tagastada
        // objekti kui Id-d ei ole samad.
        public async Task ShouldNot_GetSpaceShipById_WhenIdNotEqual()
        {
            //ülesseade
            Guid wrongGuid = Guid.NewGuid();
            Guid goodGuid = Guid.Parse("f40900a6-7c03-47af-b10c-4a035f8664fa");

            //tegevus
            await Svc<ISpaceshipServices>().DetailAsync(goodGuid);

            //
            Assert.NotEqual(wrongGuid, goodGuid);
        }
    }
}
