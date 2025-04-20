# Shopify Webhook Verifier (.NET Core)

This project is a simple ASP.NET Core API for receiving and verifying Shopify webhooks using HMAC authentication. It demonstrates how to securely validate incoming Shopify webhook events using your webhook secret.

## Features

- ASP.NET Core API controller to handle Shopify webhook calls.
- HMAC-SHA256 based verification of incoming requests.
- Basic logging support via `ILogger`.
- Extensible via the `IWebhookAuthenticationService` interface for additional auth logic.

## Requirements

- [.NET 6.0+](https://dotnet.microsoft.com/)
- Valid Shopify webhook with the correct endpoint

To create a webhook in Shopify:
1. Go to your Shopify admin panel.
2. Navigate to **Settings** > **Notifications**.
3. Scroll down to **Webhooks** and click **Create webhook**.
4. Choose the event type (e.g., **Order creation**) and set the URL to your endpoint (e.g., `https://yourdomain.com/api/webhooks/shopify/order-created`).
5. Select **JSON** as the format and set **Webhook API Version** to `2024-04`.
6. Click **Save**.

## Project Status

This project was originally built for a client but was discontinued before completion. While it's no longer actively maintained, it can serve as a reference for implementing Shopify webhook verification in .NET Core projects.
