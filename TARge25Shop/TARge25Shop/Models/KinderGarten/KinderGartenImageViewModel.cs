namespace TARge25Shop.Models.KinderGarten
{
    public class KinderGartenImageViewModel
    {
        public Guid ImageId { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public string? Image { get; set; }
        public Guid? KinderGartenId { get; set; }
    }
}