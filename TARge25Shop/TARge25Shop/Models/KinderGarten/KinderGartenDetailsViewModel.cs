namespace TARge25Shop.Models.KinderGarten
{
    public class KinderGartenDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int ChildrenCount { get; set; }
        public string KinderGartenName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;

        public List<KinderGartenImageViewModel> Images { get; set; }
            = new List<KinderGartenImageViewModel>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}