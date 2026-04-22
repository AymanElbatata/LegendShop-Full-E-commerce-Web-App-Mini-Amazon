using AymanStore.BLL.Interfaces;
using AymanStore.DAL.Contexts;
using AymanStore.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AymanStore.BLL.Repositories
{
    public class GeoLocationHelper : IGeoLocationHelper
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly HttpClient _httpClient;

        //public GeoLocationHelper(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _httpClient = httpClient;
        //}


        //public async Task<string> GetUserCountryAsync()
        //{
        //    // Get user IP
        //    var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

        //    if (string.IsNullOrEmpty(ip))
        //        return "Unknown";

        //    // Call external API (example: ipapi.co)
        //    var response = await _httpClient.GetStringAsync($"https://ipapi.co/{ip}/json/");
        //    var json = JObject.Parse(response);

        //    return json["country_name"]?.ToString() ?? "Unknown";
        //}
    }
}
