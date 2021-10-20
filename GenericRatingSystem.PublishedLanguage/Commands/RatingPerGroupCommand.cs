using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRatingSystem.PublishedLanguage.Commands
{
    public class RatingPerGroupCommand:IRequest
    {
        public string GroupId { get; set; }
        public string Category { get; set; }
        public decimal RatingAvg { get; set; }
    }
}
