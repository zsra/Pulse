namespace Pulse.Core.Settings;

public interface IMongoSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
}
