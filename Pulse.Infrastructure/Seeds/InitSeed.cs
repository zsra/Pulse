using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Models;

namespace Pulse.Infrastructure.Seeds;

public class InitSeed
{
    private readonly IUserRepository _userRepository;

    public InitSeed(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask Add()
    {
        if(!(await _userRepository.GetAllAsync()).Any())
        {
            User admin = new("admin", "admin@pulse.com", "admin", "admin", DateTime.MinValue);

            await _userRepository.CreateAsync(admin);
        }
    }
}
