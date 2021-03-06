using MediatR;

namespace GenericRatingSystem.PublishedLanguage.Events
{
    public class AccountMade: INotification
    {
        public string Name { get; set; }
    }
}
