﻿using Azure.AI.OpenAI;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SearchClientFactory _searchClientFactory;
        private readonly AzureKeyCredential _azureKeyCredential;

        private readonly AzureOpenAIClient _standardChatGptClient;

        public OpenAIClientFacade(IConfiguration configuration, AzureKeyCredential azureKeyCredential, TokenCredential tokenCredential, IHttpClientFactory httpClientFactory, SearchClientFactory searchClientFactory)
        {
            _config = configuration;
            _standardChatGptDeployment = configuration[AppConfigurationSetting.AOAIStandardChatGptDeployment];
            _standardServiceEndpoint = configuration[AppConfigurationSetting.AOAIStandardServiceEndpoint];

            _azureKeyCredential = azureKeyCredential;
            _tokenCredential = tokenCredential;
            _httpClientFactory = httpClientFactory;
            _searchClientFactory = searchClientFactory;

            if (azureKeyCredential != null)
                _standardChatGptClient = new AzureOpenAIClient(new Uri(_standardServiceEndpoint), _azureKeyCredential);
            else
                _standardChatGptClient = new AzureOpenAIClient(new Uri(_standardServiceEndpoint), _tokenCredential);
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
                kernel.ImportPluginFromObject(new DataMapperPlugins(_httpClientFactory, _config), "DATAMAPPING");
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
