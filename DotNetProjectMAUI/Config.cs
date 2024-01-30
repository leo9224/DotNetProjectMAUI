namespace DotNetProjectMAUI
{
    internal class Config
    {
        public static string APIEndpoint = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5250" : "https://localhost:7014";
    }
}
