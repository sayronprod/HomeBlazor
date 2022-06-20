namespace HomeBlazor.Services.Mqtt
{
    public class ExtarnalService
    {
        private readonly IMqttClientService mqttClientService;
        public ExtarnalService(MqttClientServiceProvider provider)
        {
            mqttClientService = provider.MqttClientService;
        }
    }
}
