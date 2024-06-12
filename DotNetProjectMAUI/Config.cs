namespace DotNetProjectMAUI
{
    internal class Config
    {
        public static string APIEndpoint = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8080" : "http://localhost:8080";
    }
}
