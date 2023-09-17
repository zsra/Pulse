using Pulse.Core.Interfaces.Models;

namespace Pulse.Core.Interfaces.Infrastructures;

public interface IRepository<TModel> where TModel : IModel
{
    TModel GetById(string id);
    IEnumerable<TModel> GetAll();
    TModel Create(TModel model);
    TModel Update(TModel model);
    void Delete(TModel model);
}
