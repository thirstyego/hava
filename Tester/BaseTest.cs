using System.Text.Json; 
namespace Tester;

public class BaseTest
{
    
    private string currentUserToken { get; set; }
    private string currentUserId { get; set; }
    protected static readonly HttpClient _httpClient = new HttpClient();
    
    private BaseTest() { }
    // private BaseTest(string currentUserToken, string currentUserId)
    // {
    //     // _httpClient.BaseAddress = new Uri("http://localhost:5249/api/");
    //     // _httpClient.Timeout = new TimeSpan(0, 0, 30);
    //     // _httpClient.DefaultRequestHeaders.Clear();
    //     //
    //     // _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    //
    //     currentUserToken = currentUserToken;
    //     currentUserId = currentUserId;
    // }
    
    public static BaseTest? singletonInstance;
    public static BaseTest GetInstance()
    {
        if (singletonInstance == null)
            singletonInstance = new BaseTest();
        return singletonInstance;
    } 
    
    
    public static HttpClient Create_HttpClient()
    {
        HttpClient client = new HttpClient(); 
        
        client.BaseAddress = new Uri("http://localhost:5249/api/");
        client.Timeout = new TimeSpan(0, 0, 30);
        client.DefaultRequestHeaders.Clear();
        
        return client;
    }
    
    
    // protected static readonly HttpClient _httpClient = new HttpClient();
    protected readonly JsonSerializerOptions _options;
    

    
    // public static HttpClient GetInstance() => _httpClient;
}