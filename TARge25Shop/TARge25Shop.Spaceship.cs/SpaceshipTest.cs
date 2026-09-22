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

        //Seleta kodus lahti, nagu eelnevate testide laused eesti keelde, selle testi oma ka...
        [Fact]
        public async Task Should_GetSpaceshipById_WhenGuidIsEqual()
        {
            //ülesseadmine
            Guid databaseGuid = Guid.Parse("f40900a6-7c03-47af-b10c-4a035f8664fa");
            Guid seekGuid = Guid.Parse("f40900a6-7c03-47af-b10c-4a035f8664fa");

            //tegevus
            await Svc<ISpaceshipServices>().DetailAsync(seekGuid);

            //kontroll
            Assert.Equal(databaseGuid, seekGuid);
        }

        [Fact]
        public async Task Should_SpaceshipDeletedById_WhenReturnedResultIsEqual()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData();

            //´tegevus
            var addSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var deleteSpaceship = await Svc<ISpaceshipServices>().Delete((Guid)addSpaceship.Id);

            //kontroll
            Assert.Equal(addSpaceship)

        }

        private SpaceshipDto MockSpaceshipData(bool isOneOrTwo = false)
        {
            if (isOneOrTwo == false)
            {
                return new SpaceshipDto
                {
                    Name = "X AE",
                    ShipType = "Lendav taldrik",
                    Crew = 67,
                    EnginePower = 69, //hobujõudu
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
            }
            else
            {
                return new SpaceshipDto
                {
                    Name = "X AE",
                    ShipType = "Lendav taldrik",
                    Crew = 67,
                    EnginePower = 69, //hobujõudu
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
            }
            
        }
    }
}
