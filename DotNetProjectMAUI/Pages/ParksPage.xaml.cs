using System.Net.Http.Json;
using DotNetProjectMAUI.Models;

namespace DotNetProjectMAUI.Pages;

public partial class ParksPage : ContentPage
{
	public ParksPage(int userId)
	{
        HttpClient httpClient = new HttpClient();
        //httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"http://10.0.2.2:5250/api/user_park/get_by_user/{userId}").Result;

        IEnumerable<UserPark>? userParks = httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<UserPark>>().Result;

        List<Park> parks = new List<Park>();

        InitializeComponent();

        if (userParks != null && userParks?.Count() != 0)
        {
            foreach (UserPark userPark in userParks)
            {
                HttpResponseMessage parkHttpResponseMessage = httpClient.GetAsync($"http://10.0.2.2:5250/api/park/{userPark.park_id}").Result;
                Park? park = parkHttpResponseMessage.Content.ReadFromJsonAsync<Park>().Result;

                if (park != null)
                {
                    parks.Add(park);
                }
            }

            ParkList.ItemsSource = parks;
        }
    }

    private async void ParkButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        int parkId = (int)button.CommandParameter;

        await Navigation.PushAsync(new RoomsPage(parkId));
    }
}