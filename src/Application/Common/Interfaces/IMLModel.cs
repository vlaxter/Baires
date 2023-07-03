namespace Baires.Application.Common.Interfaces;

public interface IMLModel<T> where T : class
{
    decimal PredictIndex(T person);
}
