public class HttpClientService
{
    private static readonly Lazy<HttpClientService> _instance = new Lazy<HttpClientService>(() => new HttpClientService());

    private readonly HttpClient _httpClient;

    private HttpClientService()
    {
        HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://10.0.2.2:7190/api/")
        };
    }

    public static HttpClientService Instance => _instance.Value;

    public HttpClient HttpClient => _httpClient;
}
