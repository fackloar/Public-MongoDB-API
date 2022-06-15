namespace MongoDB.DataLayer.Models
{
    public class ElasticSearchSettings : IElasticSearchSettings
    {
        public string Uri { get; set; } = "http://localhost:9200/";
        public string DefaultIndex { get; set; } = "default";
    }
}
