using System.Collections.Generic;

#nullable disable

namespace GenericRatingSystem.Models
{
    public partial class RatingPerGroup
    {
        public RatingPerGroup()
        {
            UserRatings = new HashSet<UserRating>();
        }

        public string GroupId { get; set; }
        public string Category { get; set; }
        public decimal RatingAvg { get; set; }

        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
