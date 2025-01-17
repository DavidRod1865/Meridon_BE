using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

public class CommodityService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly string _apiKey;
    private const string BaseUrl = "https://data.nasdaq.com/api/v3/datatables/QDL/OPEC";

    public CommodityService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
        _apiKey = Environment.GetEnvironmentVariable("Nasdaq_API_Key") ?? throw new InvalidOperationException("Nasdaq API key is not set in environment variables.");

        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new InvalidOperationException("Nasdaq API key is not set in environment variables.");
        }
    }

    public async Task<NasdaqResponse> GetPricesByDateRangeAsync(string startDate, string endDate)
    {
        // Cache key based on start and end dates
        var cacheKey = $"CommodityPrices_{startDate}_{endDate}";

        // Check if the data is in the cache
        if (_cache.TryGetValue(cacheKey, out NasdaqResponse? cachedResponse))
        {
            return cachedResponse!;
        }

        // Fetch data from the API
        var url = $"{BaseUrl}?date.gte={startDate}&date.lte={endDate}&api_key={_apiKey}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<NasdaqResponse>(content);

        if (data == null)
        {
            throw new InvalidOperationException("Failed to deserialize Nasdaq response.");
        }

        // Store the data in the cache
        _cache.Set(cacheKey, data, TimeSpan.FromMinutes(30)); // Cache for 30 minutes

        return data;
    }
}
