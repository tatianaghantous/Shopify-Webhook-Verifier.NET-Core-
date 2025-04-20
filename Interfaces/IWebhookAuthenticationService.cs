using System.Net;

namespace ShopifyAPI.Interfaces
{
    public interface IWebhookAuthenticationService
    {
         bool VerifyWebhook(string data, string hmacHeader);

    }
}
