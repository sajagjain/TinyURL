using UrlShortenerDemo.Models;

namespace UrlShortenerDemo.Services
{
    public interface IDatabase
    {
        string GetActualURL(string shortCode);
        void SaveUrl(ulong id, TinyURL tinyURL);
    }
}