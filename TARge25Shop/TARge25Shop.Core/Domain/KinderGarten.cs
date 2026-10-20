using System;
using System.Collections.Generic;
using System.Text;

namespace TARge25Shop.Core.Domain
{
    public class KinderGarten
    {
        public Guid? Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int ChildrenCount { get; set; }
        public string KinderGartenName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}