using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerConsumption.Services
{
    public interface IApiService<T, M>
    {
        Task<bool> Delete(string endpoint);
        Task<List<T>> GetAll(string endpoint);
        Task<T> Get(string enpoint);
        Task<bool> Post(M beer, string endpoint);
        Task<bool> Put(M beer, string endpoint);
    }
}