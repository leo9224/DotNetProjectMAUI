using System.Net.Http.Json;
using DotNetProjectLibrary.Models;
using Type = DotNetProjectLibrary.Models.Type;

namespace DotNetProjectMAUI.Pages;

public partial class ComputersPage : ContentPage
{
    string Token;

	public ComputersPage(int roomId, string token)
	{
        Token = token;

        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"{Config.APIEndpoint}/api/computer/get_by_room/{roomId}").Result;

        IEnumerable<Computer>? computers = httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<Computer>>().Result;

        if (computers != null && computers?.Count() != 0)
        {
            foreach (Computer computer in computers)
            {
                int? typeId = computer.type_id;

                /*if (typeId is not null)
                {
                    HttpResponseMessage httpResponseMessageForType = httpClient.GetAsync($"{Config.APIEndpoint}/api/type/{typeId}").Result;
                    Type? type = httpResponseMessageForType.Content.ReadFromJsonAsync<Type>().Result;

                    if (type is not null)
                    {
                        computer.os = type.description;
                    }
                }
                else
                {
                    computer.os = "Unknown";
                }*/
            }
        }
        else
        {
            ComputerList.IsVisible = false;
            NoItemLabel.IsVisible = true;
        }

        InitializeComponent();

        if (computers != null && computers?.Count() != 0)
        {
            ComputerList.ItemsSource = computers;
        }
    }
}