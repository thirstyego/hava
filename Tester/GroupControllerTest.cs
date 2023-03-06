using System.Text;
using System.Text.Json;
using apichipper.Models;

namespace Tester;

// TODO: dont inherit from BaseTest
public class GroupControllerTest : BaseTest
{
    
    [Theory]
    [InlineData("Test Group")]
    [InlineData("Chickens Club")]
    [InlineData("Men's house")]
    [InlineData("golden gophers cheer")]
    [InlineData("Isac")]
    public async void Create_group_test(string name)
    {
        var createGroup = new GroupPost
        {
            Name = name,
        };
        
        var serCreateGroup = JsonSerializer.Serialize(createGroup);
        var requestContent = new StringContent(
            serCreateGroup, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("group");
        var response = await client.PostAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
}