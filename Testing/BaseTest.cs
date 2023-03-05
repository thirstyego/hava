using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tester;

public class BaseTest
{
    
    public readonly ITestOutputHelper _out;

    public BaseTest(ITestOutputHelper testOutputHelper)
    {
        _out = testOutputHelper;
    } 
    
    public static string currentUserToken = "";
    public static string currentUserId = "2a13a121-44df-49f9-9727-8a6c255961c4";
    
    
    public static HttpClient Create_HttpClient()
    {
        HttpClient client = new HttpClient(); 
        
        client.BaseAddress = new Uri("http://localhost:5249/api/");
        client.Timeout = new TimeSpan(0, 0, 30);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + currentUserToken);
        
        return client;
    }
}

// public class CurrentUser
// {
//     private static CurrentUser? _currentUser;
//     public string Token { get; }
//     public string Id { get; }
//
//     public CurrentUser(string token, string id)
//     {
//         Token = token;
//         Id = id;
//     }
//     
//     public static CurrentUser GetUser()
//     {
//         Console.WriteLine("currentUser before null check: " + _currentUser);
//         if (_currentUser == null)
//         {
//             _currentUser = new CurrentUser("fdafsdsd", "df");
//         }
//         Console.WriteLine("currentUser after: " + _currentUser);
//         return _currentUser;
//     }
//
//     public static void SetUser(string token, string id)
//     {
//         _currentUser = new CurrentUser(token, id);
//     }
// }
