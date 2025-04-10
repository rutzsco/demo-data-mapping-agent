using Microsoft.SemanticKernel;
using MinimalApi.Services;
using MinimalApi.Services.Skills;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

using MinimalApi.Services.Search;
using Azure;
using Azure.Identity;


namespace Assistants.API.Core
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddAzureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SearchClientFactory>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new SearchClientFactory(config, null, new AzureKeyCredential(config[AppConfigurationSetting.AzureSearchServiceKey]));
            });
            services.AddSingleton<OpenAIClientFacade>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var standardChatGptDeployment = config["AOAIStandardChatGptDeployment"];
                var standardServiceEndpoint = config["AOAIStandardServiceEndpoint"];
                var standardServiceKey = config["AOAIStandardServiceKey"];

                if(string.IsNullOrEmpty(standardServiceKey))
                  return new OpenAIClientFacade(configuration, null, new DefaultAzureCredential());

                return new OpenAIClientFacade(configuration, new Azure.AzureKeyCredential(standardServiceKey), null);
            });
            services.AddSingleton<DataMapperChatService>();
            return services;
        }
    }
}