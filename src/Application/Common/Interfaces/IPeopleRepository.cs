using Baires.Domain.Entities;

namespace Baires.Application.Common.Interfaces;

public interface IPeopleRepository
{
    Task<IEnumerable<Person>> GetTopClientsAsync(int count, CancellationToken cancellationToken = default);
    Task<int> GetClientPositionAsync(long personId, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Person people, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(long personId, CancellationToken cancellationToken = default);
}
