﻿    
using Messages.Bll.Exceptions;
using System.Text.Json;

namespace Messages.Bll.integrations;

internal class CommonHttpClient<T>
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _options;
    
    public CommonHttpClient(string baseUrl, HttpMessageHandler? handler = null)
    {  
        if(handler != null)
        {
            _httpClient = new HttpClient(handler);
        }
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<T> GetRequest(string path)
    {
        try
        {
            var response = await _httpClient.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(content, _options);
            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new UnavailableServiceException("request to jsonplaceholder failed");
        }
    }

}
