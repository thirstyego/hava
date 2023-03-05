using System.Diagnostics;
using System.Text;
using System.Text.Json;
using hava.Models;
using Xunit.Abstractions;

namespace Tester;

public class ZoneControllerTest : BaseTest
{

    public ZoneControllerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }
    
    [Theory]
    [InlineData("nick's bedroom", 5)]
    [InlineData("master bedroom", 5)]
    [InlineData("living room", 5)]
    [InlineData("kitchen", 5)]
    public async void PostZoneTest(string name, int homeId)
    {
        
        var zone = new ZonePost
        {
            Name = name,
            HomeId = homeId
        };
        
        var serializedHome = JsonSerializer.Serialize(zone);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("Zone");
        var response = await client.PostAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(4, "ditchen", 5)]
    public async void PutZoneTest(int id, string name, int homeId)
    {
        
        var zone = new ZonePut
        {
            Id = id,
            Name = name,
            HomeId = homeId
        };
        
        var serializedHome = JsonSerializer.Serialize(zone);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("Zone");
        var response = await client.PutAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(5)]
    public async void GetHomeZonesTest(int homeId)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Zone/home/{homeId}");
        var response = await client.GetAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(4)]
    public async void GetZoneTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Zone/{id}");
        var response = await client.GetAsync(uri);
        
        _out.WriteLine("mehmetresponse");
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(3)]
    public async void DeleteZoneTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Zone/{id}");
        var response = await client.DeleteAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
}
