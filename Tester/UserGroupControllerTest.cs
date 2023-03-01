using System.Text;
using System.Text.Json;
using apichipper.Models;

namespace Tester;

public class UserGroupControllerTest : BaseTest
{
    
    [Theory]
    [InlineData("7ee7cd60-2048-4dc9-9269-ee48ccd1c0d9", 1)]
    public async void Create_group_test(string UserId, int GroupId)
    {
        var createUserGroup = new UserGroupCrud
        {
            UserId = currentUserId,
            GroupId = GroupId
        };
        
        var serCreateUserGroup = JsonSerializer.Serialize(createUserGroup);
        var requestContent = new StringContent(
            serCreateUserGroup, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("usergroup");
        var response = await client.PostAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
}