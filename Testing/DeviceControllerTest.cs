using System.Diagnostics;
using System.Text;
using System.Text.Json;
using hava.Models;
using Xunit.Abstractions;

namespace Tester;

public class DeviceControllerTest : BaseTest
{

    public DeviceControllerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }
    
    [Theory]
    [InlineData(45, true, 6)]
    [InlineData(85, true, 6)]
    [InlineData(60, false, 6)]
    [InlineData(20, true, 6)]
    public async void PostDeviceTest(int batteryPercentage, bool status, int zoneId)
    {
        
        var device = new DevicePost
        {
            BatteryPercentage = batteryPercentage,
            Status = status,
            ZoneId = zoneId
        };
        
        var serializedHome = JsonSerializer.Serialize(device);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("Device");
        var response = await client.PostAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(3, 45, true, 6)]
    public async void PutDeviceTest(int id, int batteryPercentage, bool status, int zoneId)
    {
        
        var device = new DevicePut
        {
            Id = id,
            BatteryPercentage = batteryPercentage,
            Status = status,
            ZoneId = zoneId
        };
        
        var serializedHome = JsonSerializer.Serialize(device);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("Device");
        var response = await client.PutAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(6)]
    public async void GetHomeDevicesTest(int homeId)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Device/zone/{homeId}");
        var response = await client.GetAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(8)]
    public async void GetDeviceTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Device/{id}");
        var response = await client.GetAsync(uri);
        
        _out.WriteLine("mehmetresponse");
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(7)]
    public async void DeleteDeviceTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Device/{id}");
        var response = await client.DeleteAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
}
