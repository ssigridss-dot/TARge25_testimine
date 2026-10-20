using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;
using TARge25Shop.Models.KinderGarten;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace TARge25Shop.Controllers
{
    public class KinderGartenController : Controller
    {
        private readonly IKinderGartenServices _kinderGartenServices;
        private readonly TARge25ShopContext _context;
        private readonly IFileServices _fileService;

        public KinderGartenController
            (
                IKinderGartenServices kinderGartenServices,
                TARge25ShopContext context,
                IFileServices fileService
            )
        {
            _kinderGartenServices = kinderGartenServices;
            _context = context;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            var result = _context.KinderGartens
                .Select(x => new KinderGartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KinderGartenName = x.KinderGartenName,
                    TeacherName = x.TeacherName,
                    CreatedAt = x.CreatedAt
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KinderGartenCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KinderGartenCreateUpdateViewModel vm)
        {
            var dto = new KinderGartenDto
            {
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KinderGartenName = vm.KinderGartenName,
                TeacherName = vm.TeacherName,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        KinderGartenId = x.KinderGartenId
                    }).ToArray()
            };

            var result = await _kinderGartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.DetailAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            List<KinderGartenImageViewModel> images = await FileFromDatabase(id);

            var vm = new KinderGartenCreateUpdateViewModel();

            vm.Id = kinderGarten.Id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KinderGartenName = kinderGarten.KinderGartenName;
            vm.TeacherName = kinderGarten.TeacherName;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KinderGartenCreateUpdateViewModel vm)
        {
            var dto = new KinderGartenDto
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KinderGartenName = vm.KinderGartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        KinderGartenId = x.KinderGartenId
                    }).ToArray()
            };

            var result = await _kinderGartenServices.Update(dto);

            var kinderGartenId = result.Id;

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Details), new { id = kinderGartenId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.DetailAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            List<KinderGartenImageViewModel> images = await FileFromDatabase(id);

            var vm = new KinderGartenDeleteViewModel();

            vm.Id = kinderGarten.Id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KinderGartenName = kinderGarten.KinderGartenName;
            vm.TeacherName = kinderGarten.TeacherName;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.Delete(id);

            if (kinderGarten == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.DetailAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            List<KinderGartenImageViewModel> images = await FileFromDatabase(id);

            var vm = new KinderGartenDetailsViewModel();

            vm.Id = kinderGarten.Id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KinderGartenName = kinderGarten.KinderGartenName;
            vm.TeacherName = kinderGarten.TeacherName;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            vm.Images.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(KinderGartenImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = vm.ImageId
            };

            var image = await _fileService.RemoveImageFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var kinderGartenId = image.KinderGartenId;
            return RedirectToAction(nameof(Update), new { id = kinderGartenId });
        }

        public async Task<List<KinderGartenImageViewModel>> FileFromDatabase(Guid id)
        {
            return await _context.FileToDatabases
                .Where(x => x.KinderGartenId == id)
                .Select(y => new KinderGartenImageViewModel
                {
                    ImageId = y.Id,
                    ImageTitle = y.ImageTitle,
                    ImageData = y.ImageData,
                    KinderGartenId = y.KinderGartenId,
                    Image = string.Format("data:image/gif;base64,{0}",
                        Convert.ToBase64String(y.ImageData))
                }).ToListAsync();
        }
    }




}