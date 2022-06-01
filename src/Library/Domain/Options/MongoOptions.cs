namespace Library.Domain.Options
{
    public interface IMongoOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public class MongoOptions :IMongoOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}