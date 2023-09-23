namespace Pulse.Core.Settings;

public class MongoSettings : IMongoSettings
{
    public MongoSettings(string databaseName, string connectionString)
    {
        DatabaseName = databaseName;
        ConnectionString = connectionString;
    }

    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}
