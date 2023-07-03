using Baires.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Baires.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Person> People { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
