using Luval.Coach.Data;
using Luval.Framework.Core.Configuration;
using Luval.Logging.Configuration;
using Luval.Logging.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using ConfigurationProvider = Luval.Framework.Core.Configuration.ConfigurationProvider;
using IConfigurationProvider = Luval.Framework.Core.Configuration.IConfigurationProvider;

namespace Luval.Coach
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var config = GetConfigurationProvider();
            var logging = CreateLogging();

            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<ILogger>(logging);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            /*** OAuth Implementation ***/
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = config.Get("oauth-google-client-id");
                options.ClientSecret = config.Get("oauth-google-client-secret");
                options.CallbackPath = "/signin-google";
                options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                options.ClaimActions.MapJsonKey("urn:google:image", "picture");
                BindGoogleEvents(options.Events, logging);

            });
            builder.Services.AddHttpContextAccessor();
            //builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddHttpClient();
            //builder.Services.AddScoped<HttpClient>();
            /*** OAuth Implementation ***/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            /*** OAuth Implementation ***/
            app.UseCookiePolicy();
            app.UseAuthentication();
            /*** OAuth Implementation ***/

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            logging.LogInformation("Started the application");

            app.Run();
        }

        static void BindGoogleEvents(OAuthEvents events, ILogger logger)
        {
            var ticketRecieveHandler = events.OnTicketReceived;
            var redirectToAuthorizationEndpointHander = events.OnRedirectToAuthorizationEndpoint;
            var creatingTicketHander = events.OnCreatingTicket;
            var accessDeniedHandler = events.OnAccessDenied;
            var remoteFailureHandler = events.OnRemoteFailure;

            events.OnTicketReceived = e =>
            {
                var claims = e.Principal?.Claims?.Select(i => new { Type = i.Type, Value = i.Value }).ToList();
                var json = JsonConvert.SerializeObject(new
                {
                    Claims= claims,
                    AuthenticationScheme = e.Result?.Ticket?.AuthenticationScheme,
                    Properties = e.Result?.Ticket?.Properties,
                    Succeeded = e.Result?.Succeeded,
                    FailureMessage = e.Result?.Failure?.Message,
                    Handled = e.Result?.Handled,
                    IdentityName = e.Result?.Principal?.Identity?.Name
                }, Formatting.Indented);
                logger.LogDebug($"OnTicketReceived - Data: {json}");

                return ticketRecieveHandler(e);
            };
            events.OnRedirectToAuthorizationEndpoint = e =>
            {
                var json = JsonConvert.SerializeObject(new
                {
                    RedirectUri = e.RedirectUri,
                    SchemName = e.Scheme?.Name,
                    SchemeDisplayName = e.Scheme?.DisplayName,
                    HandlerType = e.Scheme?.HandlerType.Name,
                    IssuedUtc = e.Properties?.IssuedUtc,
                    ExpiresUtc = e.Properties?.ExpiresUtc,
                    AllowRefresh = e.Properties?.AllowRefresh,
                    IsPersistent = e.Properties?.IsPersistent,
                    Items = e.Properties?.Items
                }, Formatting.Indented);

                logger.LogDebug($"OnRedirectToAuthorizationEndpoint - Data: {json}");

                return redirectToAuthorizationEndpointHander(e);

            };
            events.OnCreatingTicket = e =>
            {
                var json = JsonConvert.SerializeObject(new
                {
                    AuthenticationScheme = e.Result?.Ticket?.AuthenticationScheme,
                    Properties = e.Result?.Ticket?.Properties,
                    Succeeded = e.Result?.Succeeded,
                    FailureMessage = e.Result?.Failure?.Message,
                    IdentityName = e.Result?.Principal?.Identity?.Name
                }, Formatting.Indented);
                logger.LogDebug($"OnCreatingTicket - Data: {json}");

                return creatingTicketHander(e);

            };
            events.OnAccessDenied = e =>
            {
                var json = JsonConvert.SerializeObject(new
                {
                    AuthenticationScheme = e.Result?.Ticket?.AuthenticationScheme,
                    Properties = e.Result?.Ticket?.Properties,
                    Succeeded = e.Result?.Succeeded,
                    FailureMessage = e.Result?.Failure?.Message,
                    IdentityName = e.Result?.Principal?.Identity?.Name,
                    ReturnUrl = e.ReturnUrl,
                    ReturnUrlParameter = e.ReturnUrlParameter,
                    SchemName = e.Scheme?.Name,
                    SchemeDisplayName = e.Scheme?.DisplayName,
                    HandlerType = e.Scheme?.HandlerType.Name,

                }, Formatting.Indented);
                logger.LogDebug($"OnAccessDenied - Data: {json}");

                return accessDeniedHandler(e);

            };
            events.OnRemoteFailure = e =>
            {
                var json = JsonConvert.SerializeObject(new
                {
                    AuthenticationScheme = e.Result?.Ticket?.AuthenticationScheme,
                    Properties = e.Result?.Ticket?.Properties,
                    Succeeded = e.Result?.Succeeded,
                    FailureMessage = e.Result?.Failure?.Message,
                    IdentityName = e.Result?.Principal?.Identity?.Name,
                    SchemName = e.Scheme?.Name,
                    SchemeDisplayName = e.Scheme?.DisplayName,
                    HandlerType = e.Scheme?.HandlerType.Name,
                    IssuedUtc = e.Properties?.IssuedUtc,
                    ExpiresUtc = e.Properties?.ExpiresUtc,
                    AllowRefresh = e.Properties?.AllowRefresh,
                    IsPersistent = e.Properties?.IsPersistent,
                    Items = e.Properties?.Items

                }, Formatting.Indented);
                logger.LogDebug($"OnRemoteFailure - Data: {json}");

                return remoteFailureHandler(e);

            };

        }

        static ILogger CreateLogging()
        {
            var fileConfig = new FileConfiguration()
            {
                DirectoryName = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName
            };

            var logger = new CompositeLogger(new ILogger[] { new ColorConsoleLogger(), new FileLogger(fileConfig), new NullLogger() });
            return logger;
        }

        static IConfigurationProvider GetConfigurationProvider()
        {
            return new ConfigurationProvider(JsonFileConfigurationProvider.LoadOrCreateProd(),
                JsonFileConfigurationProvider.LoadOrCreateProd(true));
        }

    }
}