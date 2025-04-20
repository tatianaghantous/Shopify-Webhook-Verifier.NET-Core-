using System.Net;

namespace ShopifyAPI.Interfaces
{
    public interface IWebhookAuthenticationService
    {
       // bool IsRequestValid(string requestBody, string hmacHeader);
         bool VerifyWebhook(string data, string hmacHeader);
        void HandlePostRequest(HttpListenerRequest request, HttpListenerResponse response);

    }
}
