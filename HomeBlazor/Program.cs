namespace HomeBlazor
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureKestrel((serverOptions) =>
                        {
                        });
                        webBuilder.UseStartup<Startup>();
                    })
                    .UseWindowsService()
                    .Build()
                    .Run();

                return 0;
            }
            catch (Exception exception)
            {
                File.WriteAllText("/errors.txt", exception.Message + exception.StackTrace);
                Console.WriteLine(exception.Message + exception.StackTrace);
                return -1;
            }
        }
    }
}