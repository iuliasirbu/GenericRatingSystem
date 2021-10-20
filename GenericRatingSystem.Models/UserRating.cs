#nullable disable

namespace GenericRatingSystem.Models
{
    public partial class UserRating
    {
        public string UserEmail { get; set; }
        public string ExternalId { get; set; }
        public string Category { get; set; }
        public decimal Rating { get; set; }

        public virtual RatingPerGroup External { get; set; }
    }
}
