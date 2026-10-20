namespace TARge25Shop.Models.KinderGarten
{
    public class KinderGartenCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int ChildrenCount { get; set; }
        public string KinderGartenName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;

        public List<IFormFile>? Files { get; set; }
        public List<KinderGartenImageViewModel> Image { get; set; }
            = new List<KinderGartenImageViewModel>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}