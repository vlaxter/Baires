using Baires.Application.Common.Interfaces;
using Baires.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Baires.Infrastructure.Repositories;

public class PeopleRepository : IPeopleRepository
{
    private readonly IApplicationDbContext _dbContext;

    public PeopleRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsAsync(long personId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.People.AnyAsync(x => x.PersonId == personId, cancellationToken);
    }

    public async Task<IEnumerable<Person>> GetTopClientsAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _dbContext.People.OrderByDescending(x => x.PriorityIndex).Take(count).ToListAsync(cancellationToken);
    }

    public async Task<int> GetClientPositionAsync(long personId, CancellationToken cancellationToken = default)
    {
        var people = await _dbContext.People.OrderByDescending(x => x.PriorityIndex).ToListAsync(cancellationToken);
        return people.FindIndex(x => x.PersonId == personId) + 1;
    }

    public async Task<int> AddAsync(Person people, CancellationToken cancellationToken = default)
    {
        _dbContext.People.Add(people);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return people.Id;
    }
}
