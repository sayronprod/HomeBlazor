using HomeBlazor.Models;
using Zeroconf;

namespace HomeBlazor.Services
{
    public class DeviceService
    {
        public async Task<List<Device>> GetDevicesAsync()
        {
            List<Device> result = new List<Device>();
            IReadOnlyList<IZeroconfHost> devices = await ZeroconfResolver.ResolveAsync("_ledstation._tcp.local.");
            foreach (IZeroconfHost device in devices)
            {
                result.Add(new Device
                {
                    DisplayName = device.DisplayName,
                    LedStationUrl = $"http://{device.IPAddress}:{device.Services.FirstOrDefault().Value.Port}/"
                });
            }
            return result;
        }
    }
}
