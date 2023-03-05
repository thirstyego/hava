using System.Diagnostics;
using System.Text;
using System.Text.Json;
using hava.Models;
using Xunit.Abstractions;

namespace Tester;

public class CurrentEnvironmentControllerTest : BaseTest
{

    public CurrentEnvironmentControllerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }
    
    [Theory]
    [InlineData("kebab", 6)]
    [InlineData("monitor", 6)]
    [InlineData("window", 6)]
    [InlineData("sun", 6)]
    public async void PostCurrentEnvironmentTest(string name, int zoneId)
    {
        
        var currentEnvironment = new CurrentEnvironmentPost
        {
            Name = name,
            Temperature = "",
            TargetTemperature = "",
            Humidity = "",
            TargetHumidity = "",
            Mode = Mode.Heat,
            Date = DateTime.Now.ToString(),
            ZoneId = zoneId
        };
        
        var serializedHome = JsonSerializer.Serialize(currentEnvironment);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("CurrentEnvironment");
        var response = await client.PostAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(4, "sky", 6)]
    public async void PutCurrentEnvironmentTest(int id, string name, int zoneId)
    {
        
        var currentEnvironment = new CurrentEnvironmentPut
        {
            Id = id,
            Name = name,
            Temperature = "",
            TargetTemperature = "",
            Humidity = "",
            TargetHumidity = "",
            Mode = Mode.Heat,
            Date = DateTime.Now.ToString(),
            ZoneId = zoneId
        };
        
        var serializedHome = JsonSerializer.Serialize(currentEnvironment);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("CurrentEnvironment");
        var response = await client.PutAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(6)]
    public async void GetHomeCurrentEnvironmentsTest(int homeId)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"CurrentEnvironment/zone/{homeId}");
        var response = await client.GetAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(8)]
    public async void GetCurrentEnvironmentTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"CurrentEnvironment/{id}");
        var response = await client.GetAsync(uri);
        
        _out.WriteLine("mehmetresponse");
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(7)]
    public async void DeleteCurrentEnvironmentTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"CurrentEnvironment/{id}");
        var response = await client.DeleteAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
}
