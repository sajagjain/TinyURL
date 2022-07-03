namespace UrlShortenerDemo.Services
{
    public class IDGeneratorService
    {
        private ulong id = 0;

        public ulong GetNextID()
        {
            return id++ + 1 + int.MaxValue;
        }
    }
}
