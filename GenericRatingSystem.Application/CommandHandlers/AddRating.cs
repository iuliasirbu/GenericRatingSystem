using GenericRatingSystem.Data;
using GenericRatingSystem.Models;
using GenericRatingSystem.PublishedLanguage.Commands;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRatingSystem.Application.CommandHandlers
{
    public class AddRating : IRequestHandler<UsersRatingsCommand>
    {
        private readonly RatingDbContext _dbContext;

        public AddRating(RatingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UsersRatingsCommand request, CancellationToken cancellationToken)
        {
            var userRating = _dbContext.UserRatings.FirstOrDefault(p => p.ExternalId == request.ExternalId
                                                                       && p.UserEmail == request.UserEmail);
            if (userRating == null)
            {
                userRating = new UserRating
                {
                    UserEmail = request.UserEmail,
                    ExternalId = request.ExternalId,
                    Category = request.Category,
                    Rating = request.Rating
                };
                _dbContext.UserRatings.Add(userRating);
                _dbContext.SaveChanges();
            }
            else
            {
                userRating.Rating = request.Rating;
                _dbContext.UserRatings.Update(userRating);
                _dbContext.SaveChanges();
            }

         
            var average = _dbContext.UserRatings.Select(p => p.Rating).Average();

            var ratingPerConf = _dbContext.RatingPerGroups.FirstOrDefault(p => p.GroupId == request.ExternalId);
            if (ratingPerConf == null)
            {
                _dbContext.RatingPerGroups.Add(new RatingPerGroup
                {
                    GroupId = request.ExternalId,
                    Category = request.Category,
                    RatingAvg = average
                });
            }
            else
            {
                ratingPerConf.RatingAvg = average;
            }

            _dbContext.SaveChanges();
            return await Unit.Task;
        }
    }
}
