using GenericRatingSystem.PublishedLanguage.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRatingSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController:ControllerBase
    {
        private readonly MediatR.IMediator _mediator;

        public RatingController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<string> AddRating(UsersRatingsCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return "OK";
        }
    }
}
