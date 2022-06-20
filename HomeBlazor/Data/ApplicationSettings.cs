namespace HomeBlazor.Data
{
    public class ApplicationSettings
    {
        private readonly IConfiguration configuration;
        private Credential Db;

        public string DbConnectionString => configuration.GetConnectionString("DbConection").Replace("@@uid", Db.UserId).Replace("@@pass", Db.Password);

        public ApplicationSettings(IConfiguration configuration)
        {
            this.configuration = configuration;

            Db.UserId = "DevRobot";
            Db.Password = "r*@Yy2eU3@Tj";
        }
    }
    struct Credential
    {
        public string UserId;
        public string Password;
    }
}
