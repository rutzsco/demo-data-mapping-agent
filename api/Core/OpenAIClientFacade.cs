using Azure.AI.OpenAI;
using Azure.Core;
using Azure;
using Microsoft.SemanticKernel;
using MinimalApi.Services.Search;
using MinimalApi.Services.Skills;
namespace Assistants.API.Core
{
    public class OpenAIClientFacade
    {
        private readonly IConfiguration _config;
        private readonly string _standardChatGptDeployment;
        private readonly string _standardServiceEndpoint;
        private readonly TokenCredential _tokenCredential;
        private readonly AzureKeyCredential _azureKeyCredential;

        public OpenAIClientFacade(IConfiguration configuration, AzureKeyCredential azureKeyCredential, TokenCredential tokenCredential)
        {
            _config = configuration;
            _standardChatGptDeployment = configuration[AppConfigurationSetting.AOAIStandardChatGptDeployment];
            _standardServiceEndpoint = configuration[AppConfigurationSetting.AOAIStandardServiceEndpoint];

            _azureKeyCredential = azureKeyCredential;
            _tokenCredential = tokenCredential;
        }

        public string GetKernelDeploymentName()
        {
            return _standardChatGptDeployment;
        }

        public Kernel BuildKernel(string toolPackage)
        {
            var kernel = BuildKernelBasedOnIdentity();
            if (toolPackage == "DATAMAPPING")
            {
                kernel.ImportPluginFromObject(new DataMapperPlugins(_config), "DATAMAPPING");
            }

            return kernel;
        }

        private Kernel BuildKernelBasedOnIdentity()
        {
            if (_azureKeyCredential != null)
            {
                var keyKernel = Kernel.CreateBuilder()
                    .AddAzureOpenAIChatCompletion(_standardChatGptDeployment, _standardServiceEndpoint, _config[AppConfigurationSetting.AOAIStandardServiceKey])
                    .Build();
                return keyKernel;
            }

            var kernel = Kernel.CreateBuilder()
           .AddAzureOpenAIChatCompletion(_standardChatGptDeployment, _standardServiceEndpoint, _tokenCredential)
           .Build();

            return kernel;
        }
    }

}
