using DotNetProjectAPI.Models;
using DotNetProjectMAUI.Models;
using System.Net.Http.Json;
using Type = DotNetProjectAPI.Models.Type;

namespace DotNetProjectMAUI.Pages;

public partial class ComputersPage : ContentPage
{
	public ComputersPage(int roomId)
	{
        HttpClient httpClient = new HttpClient();
        //httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"http://10.0.2.2:5250/api/computer/get_by_room/{roomId}").Result;

        IEnumerable<Computer>? computers = httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<Computer>>().Result;

        if (computers is not null)
        {
            foreach (Computer computer in computers)
            {
                int? typeId = computer.type_id;

                if (typeId is not null)
                {
                    HttpResponseMessage httpResponseMessageForType = httpClient.GetAsync($"http://10.0.2.2:5250/api/type/{typeId}").Result;
                    Type? type = httpResponseMessageForType.Content.ReadFromJsonAsync<Type>().Result;

                    if (type is not null)
                    {
                        computer.os = type.description;
                    }
                }
                else
                {
                    computer.os = "Unknown";
                }
            }
        }

        InitializeComponent();

        if (computers != null && computers?.Count() != 0)
        {
            ComputerList.ItemsSource = computers;
        }
    }
}