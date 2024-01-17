using DotNetProjectMAUI.Models;
using System.Net.Http.Json;

namespace DotNetProjectMAUI.Pages;

public partial class ComputersPage : ContentPage
{
	public ComputersPage(int roomId)
	{
        HttpClient httpClient = new HttpClient();
        //httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"http://10.0.2.2:5250/api/computer/get_by_room/{roomId}").Result;

        IEnumerable<Computer>? computers = httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<Computer>>().Result;

        InitializeComponent();

        if (computers != null && computers?.Count() != 0)
        {
            ComputerList.ItemsSource = computers;
        }
    }
}