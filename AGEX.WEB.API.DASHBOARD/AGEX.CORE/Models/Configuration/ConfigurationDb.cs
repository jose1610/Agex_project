namespace AGEX.CORE.Models.Configuration
{
    public class ConfigurationDb
    {
        public DbModel AgexDB { get; set; }
    }

    public class DbModel
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
    }
}
