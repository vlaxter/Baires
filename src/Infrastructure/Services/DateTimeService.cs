using Baires.Application.Common.Interfaces;

namespace Baires.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
