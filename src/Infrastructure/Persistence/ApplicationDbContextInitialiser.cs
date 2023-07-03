using Baires.Application.Common.Interfaces;
using Baires.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Baires.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IJsonFileDeserializer<Person> _personJsonFileDeserializerService;
    private readonly IMLModel<Person> _personMLModel;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger, 
        ApplicationDbContext context, 
        IJsonFileDeserializer<Person> personJsonFileService,
        IMLModel<Person> personMLModel)
    {
        _logger = logger;
        _context = context;
        _personJsonFileDeserializerService = personJsonFileService;
        _personMLModel = personMLModel;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Seed, if necessary
        if (!_context.People.Any())
        {
            try
            {
                var people = _personJsonFileDeserializerService.Deserialize("Data\\people.json");
                if (people != null)
                {
                    people.ToList().ForEach(p => p.PriorityIndex = _personMLModel.PredictIndex(p));

                    await _context.People.AddRangeAsync(people);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
