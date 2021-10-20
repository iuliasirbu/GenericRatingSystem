using MediatR;

namespace GenericRatingSystem.PublishedLanguage.Commands
{
    public class UsersRatingsCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string ExternalId { get; set; }
        public string Category { get; set; }
        public decimal Rating { get; set; }
    }
}
