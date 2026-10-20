using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;

namespace TARge25Shop.ApplicationServices.Services
{
    public class KinderGartenServices : IKinderGartenServices
    {
        private readonly TARge25ShopContext _context;
        private readonly IFileServices _fileServices;

        public KinderGartenServices
            (
                TARge25ShopContext context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<KinderGarten> Create(KinderGartenDto dto)
        {
            KinderGarten kinderGarten = new();

            kinderGarten.Id = Guid.NewGuid();
            kinderGarten.GroupName = dto.GroupName;
            kinderGarten.ChildrenCount = dto.ChildrenCount;
            kinderGarten.KinderGartenName = dto.KinderGartenName;
            kinderGarten.TeacherName = dto.TeacherName;
            kinderGarten.CreatedAt = DateTime.Now;
            kinderGarten.UpdatedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadKinderGartenFilesToDatabase(dto, kinderGarten);
            }

            await _context.KinderGartens.AddAsync(kinderGarten);
            await _context.SaveChangesAsync();

            return kinderGarten;
        }

        public async Task<KinderGarten> Update(KinderGartenDto dto)
        {
            // 1. SA PEAD ESITEKS ANDMEBAASIST OLEMASOLEVA KIRJE ÜLES OTSIMA ID JÄRGI!
            var kinderGarten = await _context.KinderGartens.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (kinderGarten == null)
            {
                return null; // Kui siia satub, siis tähendabki, et ID-ga kirjet ei leitud
            }

            // 2. Kanda DTO andmed ümber leitud objektile
            kinderGarten.GroupName = dto.GroupName;
            kinderGarten.ChildrenCount = dto.ChildrenCount;
            kinderGarten.KinderGartenName = dto.KinderGartenName;
            kinderGarten.TeacherName = dto.TeacherName;
            kinderGarten.UpdatedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadKinderGartenFilesToDatabase(dto, kinderGarten);
            }

            // 3. Uuenda ja salvesta
            _context.KinderGartens.Update(kinderGarten);
            await _context.SaveChangesAsync();

            return kinderGarten;
        }

        public async Task<KinderGarten> DetailAsync(Guid id)
        {
            var kinderGarten = await _context.KinderGartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return kinderGarten;
        }

        public async Task<KinderGarten> Delete(Guid id)
        {
            var kinderGarten = await _context.KinderGartens
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToDatabases
                .Where(x => x.KinderGartenId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromDatabase(images);
            _context.KinderGartens.Remove(kinderGarten);
            await _context.SaveChangesAsync();

            return kinderGarten;
        }
    }
}