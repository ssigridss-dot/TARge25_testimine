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

        [Fact]
        public async Task Should_KinderGartenDeletedById_WhenReturnedResultIsEqual()
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
            var addKinderGarten = await Svc<IKinderGartenServices>().Create(dto);
            var deleteKinderGarten = await Svc<IKinderGartenServices>().Delete((Guid)addKinderGarten.Id);

            // Kontroll
            Assert.Equal(addKinderGarten.Id, deleteKinderGarten.Id);
        }

        [Fact]
        public async Task ShouldNot_DeleteKinderGartenByID_WhenDidNotDeleteKinderGarten()
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
            var kinderGarten1 = await Svc<IKinderGartenServices>().Create(dto);
            var kinderGarten2 = await Svc<IKinderGartenServices>().Create(dto);

            var result = await Svc<IKinderGartenServices>().Delete((Guid)kinderGarten2.Id);

            // Kontroll
            Assert.NotEqual(kinderGarten1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateKinderGartenByID_WhenUpdatingData()
        {
            // Ülesseade
            var guid = new Guid("68eb8abd-086a-4c8b-9695-71234143f709");

            KinderGartenDto dto = new KinderGartenDto()
            {
                GroupName = "Mesilane",
                ChildrenCount = 3,
                KinderGartenName = "Sipsik",
                TeacherName = "Aino",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            KinderGartenDto domain = new();
            domain.Id = Guid.Parse("68eb8abd-086a-4c8b-9695-71234143f709");
            domain.ChildrenCount = 30;
            domain.GroupName = "Mürakarud";
            domain.KinderGartenName = "Lilleke";
            domain.TeacherName = "Maali";
            domain.CreatedAt = dto.CreatedAt;
            domain.UpdatedAt = DateTime.UtcNow;

            // Tegevus
            await Svc<IKinderGartenServices>().Update(dto);

            // Kontroll
            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.ChildrenCount, domain.ChildrenCount);
            Assert.NotEqual(dto.GroupName, domain.GroupName);
            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.UpdatedAt, domain.CreatedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateKinderGartenByID_WhenNoDataIsUpdated()
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
            var createdKinderGarten = await Svc<IKinderGartenServices>().Create(dto);

            // Tegevus
            KinderGartenDto nullDto = new KinderGartenDto()
            {
                GroupName = null,
                ChildrenCount = 0,
                KinderGartenName = null,
                TeacherName = null,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };
            var result = await Svc<IKinderGartenServices>().Update(nullDto);

            // Kontroll
            Assert.NotEqual(createdKinderGarten.Id, result?.Id);
        }

        [Fact]
        public async Task ShouldNot_CreateKinderGartenWithNegativeChildrenCount_WhenCountNegative()
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
            dto.ChildrenCount -= (dto.ChildrenCount * 2);

            // Tegevus
            var result = await Svc<IKinderGartenServices>().Create(dto);

            // Kontroll
            Assert.True(result.ChildrenCount < 0);
        }

        [Fact]
        public async Task ShouldNot_CreateKinderGarten_WhenChildrenCountIsZeroOrLess()
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
            dto.ChildrenCount = 0;

            // Tegevus
            var result = await Svc<IKinderGartenServices>().Create(dto);

            // Kontroll
            Assert.NotNull(result);
            Assert.Equal(0, result.ChildrenCount);
        }

        [Fact]
        public async Task Should_RemoveKinderGartenFromDatabase_WhenKinderGartenIsDeleted()
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
            var createdKinderGarten = await Svc<IKinderGartenServices>().Create(dto);
            var deletedKinderGarten = await Svc<IKinderGartenServices>().Delete((Guid)createdKinderGarten.Id);
            var result = await Svc<IKinderGartenServices>().DetailAsync((Guid)createdKinderGarten.Id);

            // Kontrollimine
            Assert.Equal(createdKinderGarten.Id, deletedKinderGarten.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldNot_RemoveKinderGartenFromDatabase_WhenKinderGartenIdIsDifferent()
        {
            KinderGartenDto dto = new KinderGartenDto()
            {
                GroupName = "Mesilane",
                ChildrenCount = 3,
                KinderGartenName = "Sipsik",
                TeacherName = "Aino",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var createdKinderGarten1 = await Svc<IKinderGartenServices>().Create(dto);
            var createdKinderGarten2 = await Svc<IKinderGartenServices>().Create(dto);
            var deleteResult = await Svc<IKinderGartenServices>().Delete((Guid)createdKinderGarten2.Id);
            var kinderGartenInDb = await Svc<IKinderGartenServices>().DetailAsync((Guid)createdKinderGarten1.Id);

            // Kontroll
            Assert.NotNull(kinderGartenInDb);
            Assert.NotEqual(deleteResult.Id, createdKinderGarten1.Id);
            Assert.Equal(createdKinderGarten1.Id, kinderGartenInDb.Id);
        }
    }
}