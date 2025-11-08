using Microsoft.AspNetCore.Mvc;
using UrlShortner.data;
using UrlShortner.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace UrlShortner.controllers
{
    [ApiController]
    [Route("")]
    public class UrlController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IDistributedCache _cache;

        public UrlController(AppDbContext db, IDistributedCache cache)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> Shorten([FromBody] string url)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(url))
                return BadRequest(new { error = "URL cannot be empty" });

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || 
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                return BadRequest(new { error = "Invalid URL format" });

            var shortid = Guid.NewGuid().ToString("N")[..8];
            var entry = new UrlMapping { ShortId = shortid, OriginalUrl = url };

            _db.UrlMappings.Add(entry);
            await _db.SaveChangesAsync();

            // Cache the mapping
            await _cache.SetStringAsync(shortid, url, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });

            return Ok(new { shortUrl = $"{Request.Scheme}://{Request.Host}/{shortid}" });
        }

        [HttpGet("{shortId}")]
        public async Task<IActionResult> RedirectToOriginal(string shortId)
        {
            string? cached = await _cache.GetStringAsync(shortId);
            if (cached != null) return Redirect(cached);

            var mapping = await _db.UrlMappings.FirstOrDefaultAsync(u => u.ShortId == shortId);
            if (mapping == null) return NotFound();

            await _cache.SetStringAsync(shortId, mapping.OriginalUrl);
            return Redirect(mapping.OriginalUrl);
        }

    }
}