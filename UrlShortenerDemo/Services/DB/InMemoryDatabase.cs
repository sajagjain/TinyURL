using UrlShortenerDemo.Models;

namespace UrlShortenerDemo.Services
{
    public class InMemoryDatabase : IDatabase
    {
        private static Dictionary<ulong, (string ShortCode, string ActualUrl)> db = new();

        public void SaveUrl(ulong id, TinyURL tinyURL)
        {
            db.Add(id, (tinyURL.ShortCode, tinyURL.ActualUrl));
        }

        public string GetActualURL(string shortCode)
        {
            return db.FirstOrDefault(kvPair => kvPair.Value.ShortCode.Equals(shortCode)).Value.ActualUrl;
        }
    }
}
