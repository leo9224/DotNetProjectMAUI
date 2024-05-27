using System.Net.Http.Json;
using DotNetProjectLibrary.Models;

namespace DotNetProjectMAUI.Pages;

public partial class RoomsPage : ContentPage
{
    string Token;

	public RoomsPage(int parkId, string token)
	{
        Token = token;

        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"{Config.APIEndpoint}/api/room/get_by_park/{parkId}").Result;

        IEnumerable<Room>? rooms = httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<Room>>().Result;

        InitializeComponent();

        if (rooms != null && rooms?.Count() != 0)
        {
            RoomList.ItemsSource = rooms;
        }
    }

    private async void RoomButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        int roomId = (int)button.CommandParameter;

        await Navigation.PushAsync(new ComputersPage(roomId, Token));
    }
}