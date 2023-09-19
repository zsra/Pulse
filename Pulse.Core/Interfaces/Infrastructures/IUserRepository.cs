using Pulse.Core.Models;

namespace Pulse.Core.Interfaces.Infrastructures;

public interface IUserRepository : IRepository<User>
{
    ValueTask<User> GetUserByEmailAsync(string email);
}
