using GenericRatingSystem.Data;
using GenericRatingSystem.Models;
using GenericRatingSystem.PublishedLanguage.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRatingSystem.Application.CommandHandlers
{
    public class RatingAverage : IRequestHandler<RatingPerGroupCommand>
    {
        private readonly RatingDbContext _dbContext;

        public RatingAverage(RatingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(RatingPerGroupCommand request, CancellationToken cancellationToken)
        {
            var average = _dbContext.UserRatings.Where(x => x.ExternalId == request.GroupId).Average(x => x.Rating);

            RatingPerGroup ratingPerGroup = new()
            {
                GroupId = request.GroupId,
                Category = request.Category,
                RatingAvg = average
            };

            _dbContext.RatingPerGroups.Add(ratingPerGroup);
            _dbContext.SaveChanges();

            return await Unit.Task;

        }
    }
}
