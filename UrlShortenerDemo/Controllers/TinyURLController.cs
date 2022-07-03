using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UrlShortener.Domain;
using UrlShortenerDemo.Services;

namespace UrlShortenerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinyURLController : ControllerBase
    {
        private readonly TinyURLGenerator _urlGenerator;
        private readonly IDatabase _database;
        private readonly IDGeneratorService _idGeneratorService;
        public TinyURLController(TinyURLGenerator urlGenerator, IDatabase database, 
            IDGeneratorService idGeneratorService)
        {
            _urlGenerator = urlGenerator;
            _database = database;
            _idGeneratorService = idGeneratorService;
        }

        [HttpGet]
        [Route("/{shortCode}")]
        public void RedirectToActual(string shortCode)
        {
            Response.StatusCode = 302;
            Response.Headers.Location = _database.GetActualURL(shortCode);
        }

        [HttpPost]
        public string CreateTinyURL(string actualUrl)
        {
            ulong nextId = _idGeneratorService.GetNextID();
            string shortCode = _urlGenerator.Shorten(nextId);
            _database.SaveUrl(nextId, new (shortCode, actualUrl));
            return shortCode;
        }
    }
}
