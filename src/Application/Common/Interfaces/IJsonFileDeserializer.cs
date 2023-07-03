namespace Baires.Application.Common.Interfaces;

public interface IJsonFileDeserializer<T> where T : class
{
    IEnumerable<T> Deserialize(string relativePath);
}
