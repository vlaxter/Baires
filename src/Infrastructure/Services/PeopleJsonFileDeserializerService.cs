using System.Text.Json;
using Baires.Application.Common.Interfaces;
using Baires.Domain.Entities;

namespace Baires.Infrastructure.Services;

public class PeopleJsonFileDeserializerService : IJsonFileDeserializer<Person>
{
    public IEnumerable<Person> Deserialize(string relativePath)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

        using var jsonFileReader = File.OpenText(path);

        var people = JsonSerializer.Deserialize<Person[]>(jsonFileReader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ;

        return people ?? Enumerable.Empty<Person>();
    }
}
