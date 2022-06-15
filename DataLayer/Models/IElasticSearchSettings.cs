namespace MongoDB.DataLayer.Models
{
    public interface IElasticSearchSettings
    {
        string Uri { get; set; }
        string DefaultIndex { get; set; }

    }
}
