namespace TrailTracker.API.Models
{
    public interface ITrailTrackerDatabaseSettings
    {
        string TrailsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }

    public class TrailTrackerDatabaseSettings : ITrailTrackerDatabaseSettings
    {
        public string TrailsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
