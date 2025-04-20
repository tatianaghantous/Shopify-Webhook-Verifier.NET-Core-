using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using ShopifyAPI.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NuGet.Common;
using System.Security.Cryptography;
using Azure.Core;
using System.Collections.Specialized;
using System;

[ApiController]
[Route("api/webhooks/shopify")]
public class WebhooksController : ControllerBase
{
    private readonly IWebhookAuthenticationService _webhookAuthenticationService;
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<WebhooksController> _logger;
    private const string CLIENT_SECRET = "e6962cdcf571d0de35fe04de1f08acc07024de30abe7e63443f3fd1220c97d51";
    public WebhooksController(IWebhookAuthenticationService webhookAuthenticationService, IInventoryService inventoryService, ILogger<WebhooksController> logger)
    {
        _webhookAuthenticationService = webhookAuthenticationService;
        _inventoryService = inventoryService;
        _logger = logger;
    }

    [HttpPost("order-created")]
    public async Task<IActionResult> HandleWebhook()
    {
        var hmacHeader = Request.Headers["X-Shopify-Hmac-SHA256"].ToString();

        if (await VerifyWebhookAsync(Request.Body, hmacHeader))
        {
            return Ok(); // Successfully verified
        }

        return Unauthorized(); // HMAC verification failed
    }
    private async Task<bool> VerifyWebhookAsync(Stream dataStream, string hmacHeader)
    {
        using (var memoryStream = new MemoryStream())
        {
            await dataStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var dataBytes = memoryStream.ToArray();
            var computedHmac = ComputeHmac(dataBytes, CLIENT_SECRET);
            return SecureCompare(computedHmac, hmacHeader);
        }
    }

    private string ComputeHmac(byte[] data, string secret)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secret);

        using (var hmac = new HMACSHA256(keyBytes))
        {
            var hashBytes = hmac.ComputeHash(data);
            return Convert.ToBase64String(hashBytes);
        }
    }

    private bool SecureCompare(string computedHmac, string hmacHeader)
    {
        return string.Equals(computedHmac, hmacHeader, StringComparison.OrdinalIgnoreCase);
    }
}


       
