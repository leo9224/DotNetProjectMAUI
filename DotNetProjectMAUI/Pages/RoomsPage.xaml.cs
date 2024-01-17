using DotNetProjectMAUI.Models;
using System.Net.Http.Json;

namespace DotNetProjectMAUI.Pages;

public partial class RoomsPage : ContentPage
{
	public RoomsPage(int parkId)
	{
        HttpClient httpClient = new HttpClient();
        //httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"http://10.0.2.2:5250/api/room/get_by_park/{parkId}").Result;

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

        await Navigation.PushAsync(new ComputersPage(roomId));
    }
}