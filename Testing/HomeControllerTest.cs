// using System.Text;
// using System.Text.Json;
// using apichipper.Models;
//
// namespace Tester;
//
// public class HomeControllerTest : BaseTest
// {
//     
//     
//     [Theory]
//     [InlineData("Test Group", 1)]
//     [InlineData("Chickens Club", 1)]
//     [InlineData("Reddit bums", 2)]
//     [InlineData("Julie and Rob", 2)]
//     [InlineData("Power Couple", 3)]
//     [InlineData("Sinister chickens", 4)]
//     public async void CreateHomeTest(string ApplicationUserId)
//     {
//         var createExpense = new Expense
//         {
//             Id = 0,
//         };
//         
//         var serCreateExpense = JsonSerializer.Serialize(createExpense);
//         var requestContent = new StringContent(
//             serCreateExpense, Encoding.UTF8, "application/json");
//         
//         var client = Create_HttpClient();
//         Console.WriteLine("current user token: " + currentUserToken);
//         client.DefaultRequestHeaders.Add("Authorization", "Bearer " + currentUserToken);
//         var uri = Path.Combine("expense");
//         var response = await client.PostAsync(uri, requestContent);
//         
//         response.EnsureSuccessStatusCode();
//     }
// }