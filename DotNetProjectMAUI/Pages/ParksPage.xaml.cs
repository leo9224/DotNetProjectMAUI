using System.Net.Http.Json;
using DotNetProjectLibrary.Models;

namespace DotNetProjectMAUI.Pages;

public partial class ParksPage : ContentPage
{
    string Token;

	public ParksPage(int userId, string token)
	{
        Token = token;

        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"{Config.APIEndpoint}/api/user_park/get_by_user/{userId}").Result;

        IEnumerable<UserPark>? userParks = httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<UserPark>>().Result;

        List<Park> parks = new List<Park>();

        InitializeComponent();

        if (userParks != null && userParks?.Count() != 0)
        {
            foreach (UserPark userPark in userParks!)
            {
                HttpResponseMessage parkHttpResponseMessage = httpClient.GetAsync($"{Config.APIEndpoint}/api/park/{userPark.park_id}").Result;
                Park? park = parkHttpResponseMessage.Content.ReadFromJsonAsync<Park>().Result;

                if (park != null)
                {
                    parks.Add(park);
                }
            }

            ParkList.ItemsSource = parks;
        }
        else
        {
            ParkList.IsVisible = false;
            NoItemLabel.IsVisible = true;
        }
    }

    private async void ParkButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        int parkId = (int)button.CommandParameter;

        await Navigation.PushAsync(new RoomsPage(parkId, Token));
    }
}