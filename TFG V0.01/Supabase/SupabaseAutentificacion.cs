using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SupabaseAutentificacion
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _apiKey;

    public SupabaseAutentificacion(string supabaseUrl, string apiKey)
    {
        _supabaseUrl = supabaseUrl.TrimEnd('/');
        _apiKey = apiKey;

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("apikey", _apiKey);

    }

    public async Task<string> SignUpAsync(string email, string password)
    {
        var url = $"{_supabaseUrl}/auth/v1/signup";

        var payload = new
        {
            email,
            password
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"SignUp error: {result}");

        return result;
    }

    public async Task<string> SignInAsync(string email, string password)
    {
        var url = $"{_supabaseUrl}/auth/v1/token?grant_type=password";

        var payload = new
        {
            email,
            password
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"SignIn error: {result}");

        return result;
    }

    
}
