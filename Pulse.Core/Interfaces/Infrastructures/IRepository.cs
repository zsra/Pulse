using Pulse.Core.Interfaces.Models;

namespace Pulse.Core.Interfaces.Infrastructures;

public interface IRepository<TModel> where TModel : IModel
{
    ValueTask<TModel> GetByIdAsync(string id);
    ValueTask<IEnumerable<TModel>> GetAllAsync();
    ValueTask<TModel> CreateAsync(TModel model);
    ValueTask<TModel> UpdateAsync(TModel model);
    ValueTask DeleteAsync(TModel model);
}
