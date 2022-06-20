using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using HomeBlazor.Data;
using HomeBlazor.Extensions;
using HomeBlazor.Mapping;
using HomeBlazor.Services;
using HomeBlazor.Services.Mqtt;
using HomeBlazor.Settings;
using Microsoft.EntityFrameworkCore;

namespace HomeBlazor
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MapConfiguration();
        }

        private void MapConfiguration()
        {
            MapBrokerHostSettings();
            MapClientSettings();
        }

        private void MapBrokerHostSettings()
        {
            BrokerHostSettings brokerHostSettings = new BrokerHostSettings();
            Configuration.GetSection(nameof(BrokerHostSettings)).Bind(brokerHostSettings);
            AppSettingsProvider.BrokerHostSettings = brokerHostSettings;
        }

        private void MapClientSettings()
        {
            ClientSettings clientSettings = new ClientSettings();
            Configuration.GetSection(nameof(ClientSettings)).Bind(clientSettings);
            AppSettingsProvider.ClientSettings = clientSettings;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectinStrings = new ApplicationSettings(Configuration);
            services.AddSingleton<ApplicationSettings>();

            services.AddAutoMapper(typeof(AppMappingProfile));

            services.AddDbContext<WeatherContext>(options =>
            {
                options.UseSqlServer(connectinStrings.DbConnectionString);
            });
            services.AddScoped<WeatherRepository>();

            services.AddSingleton<WeatherService>();
            services.AddScoped<WeatherViewService>();

            services.AddMqttClientHostedService();
            services.AddSingleton<ExtarnalService>();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddBlazorise(options =>
            {
            });
            services.AddBootstrapProviders();
            services.AddFontAwesomeIcons();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
