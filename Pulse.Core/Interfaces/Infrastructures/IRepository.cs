using Pulse.Core.Interfaces.Models;

namespace Pulse.Core.Interfaces.Infrastructures;

public interface IRepository<TModel> where TModel : IModel
{
    ValueTask<TModel> GetById(string id);
    ValueTask<IEnumerable<TModel>> GetAll();
    ValueTask<TModel> Create(TModel model);
    ValueTask<TModel> Update(TModel model);
    ValueTask Delete(TModel model);
}
