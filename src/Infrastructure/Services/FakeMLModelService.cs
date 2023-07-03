using System.Security.Cryptography;
using System.Text;
using Baires.Application.Common.Interfaces;
using Baires.Domain.Entities;

namespace Baires.Infrastructure.Services;

public class FakeMLModelService : IMLModel<Person>
{
    public decimal PredictIndex(Person person)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(person.PersonId.ToString());
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var stringHash = Convert.ToHexString(hashBytes);
            var index = stringHash.Substring(0, 10).Select((x, i) => i * (int)x).Sum() / 6710m;
            return index;
        }
    }
}
