using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNetProjectMAUI.Pages;

public partial class AuthenticationPage : ContentPage
{
	public AuthenticationPage()
	{
		InitializeComponent();
	}

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        HttpClient httpClient = new HttpClient();
		HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://10.0.2.2:5250/api/authentication/{EmailInput.Text}/{PasswordInput.Text}");
        
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            string stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            JObject jsonResponse = JObject.Parse(stringResponse);

            string token = $"Bearer {jsonResponse.GetValue("token")}";
            int userId = jsonResponse.GetValue("user_id")!.ToObject<int>();

            await DisplayAlert("Login", token, "Ok");
            await Navigation.PushAsync(new ParksPage(userId));
        }
        else
        {
            InvalidCredentialsError.IsVisible = true;
        }
    }

    private void EmailInputTextChanged(object sender, TextChangedEventArgs e)
    {
        CheckLoginInputs();
    }

    private void PasswordInputTextChanged(object sender, TextChangedEventArgs e)
    {
        CheckLoginInputs();
    }

	private void CheckLoginInputs()
	{
        if (EmailInput.Text != null && EmailInput.Text != string.Empty)
        {
            if (PasswordInput.Text != null && PasswordInput.Text != string.Empty)
            {
                LoginButton.IsEnabled = true;
                return;
            }

            LoginButton.IsEnabled = false;
        }

        LoginButton.IsEnabled = false;
    }
}