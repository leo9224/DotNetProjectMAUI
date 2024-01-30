using DotNetProjectLibrary.Authentication;
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
        Console.WriteLine($"{Config.APIEndpoint}/api/authentication/{EmailInput.Text}/{PasswordInput.Text}");
        HttpClient httpClient = new HttpClient();
		HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"{Config.APIEndpoint}/api/authentication/{EmailInput.Text}/{PasswordInput.Text}");
        
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
        if (Authentication.CheckValidEmail(EmailInput.Text) && Authentication.CheckValidPassword(PasswordInput.Text))
        {
            LoginButton.IsEnabled = true;
        }
        else
        {
            LoginButton.IsEnabled = false;
        }
    }
}